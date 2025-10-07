using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Unity.Services.Relay.Models;
using Utp;
using Player;
using System.Collections;

public class RelayNetworkManager : NetworkManager
{
	[SerializeField] public List<NetworkIdentity> listOfNetworkIden = new List<NetworkIdentity>();
	public PlayerObject hostPlayer;
	private UtpTransport utpTransport;

	/// <summary>
	/// Server's join code if using Relay.
	/// </summary>
	public string relayJoinCode = "";

	public override void Awake()
	{
		base.Awake();

		utpTransport = GetComponent<UtpTransport>();

		string[] args = System.Environment.GetCommandLineArgs();
		for (int key = 0; key < args.Length; key++)
		{
			if (args[key] == "-port")
			{
				if (key + 1 < args.Length)
				{
					string value = args[key + 1];

					try
					{
						utpTransport.Port = ushort.Parse(value);
					}
					catch
					{
						UtpLog.Warning($"Unable to parse {value} into transport Port");
					}
				}
			}
		}
	}

	#region StartServer
	/// <summary>
	/// Get the port the server is listening on.
	/// </summary>
	/// <returns>The port.</returns>
	public ushort GetPort()
	{
		return utpTransport.Port;
	}

	/// <summary>
	/// Get whether Relay is enabled or not.
	/// </summary>
	/// <returns>True if enabled, false otherwise.</returns>
	public bool IsRelayEnabled()
	{
		return utpTransport.useRelay;
	}

	/// <summary>
	/// Ensures Relay is disabled. Starts the server, listening for incoming connections.
	/// </summary>
	public void StartStandardServer()
	{
		utpTransport.useRelay = false;
		StartServer();
	}

	/// <summary>
	/// Ensures Relay is disabled. Starts a network "host" - a server and client in the same application
	/// </summary>
	public void StartStandardHost()
	{
		utpTransport.useRelay = false;
		StartHost();
	}

	/// <summary>
	/// Gets available Relay regions.
	/// </summary>
	/// 
	public void GetRelayRegions(Action<List<Region>> onSuccess, Action onFailure)
	{
		utpTransport.GetRelayRegions(onSuccess, onFailure);
	}

	/// <summary>
	/// Ensures Relay is enabled. Starts a network "host" - a server and client in the same application
	/// </summary>
	public void StartRelayHost(int maxPlayers, string regionId = null, Action callback = null)
	{
		utpTransport.useRelay = true;
		utpTransport.AllocateRelayServer(maxPlayers, regionId,
		(string joinCode) =>
		{
			relayJoinCode = joinCode;

			StartHost();
			callback?.Invoke();
		},
		() =>
		{
			UtpLog.Error($"Failed to start a Relay host.");
		});
	}

	/// <summary>
	/// Ensures Relay is disabled. Starts the client, connects it to the server with networkAddress.
	/// </summary>
	public void JoinStandardServer()
	{
		utpTransport.useRelay = false;
		StartClient();
	}

	/// <summary>
	/// Ensures Relay is enabled. Starts the client, connects to the server with the relayJoinCode.
	/// </summary>
	public void JoinRelayServer(string _joinCode, Action callback = null)
	{
		utpTransport.useRelay = true;
		utpTransport.ConfigureClientWithJoinCode(_joinCode,
		() =>
		{
			StartClient();
			callback?.Invoke();
		},
		() =>
		{
			UtpLog.Error($"Failed to join Relay server.");
		});
	}
	#endregion

	public override void OnStartServer()
	{
		base.OnStartServer();

		foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
		{
			if (conn.identity != null)
			{
				AddPlayerIden(conn.identity);
			}
		}
	}
	public override void OnServerAddPlayer(NetworkConnectionToClient conn)
	{
		base.OnServerAddPlayer(conn);
		if (conn.identity != null && !listOfNetworkIden.Contains(conn.identity))
		{
			AddPlayerIden(conn.identity);
		}
		StartCoroutine(WaitAndRegisterPlayer(conn));
	}
	public override void OnServerDisconnect(NetworkConnectionToClient conn)
	{
		if (conn.identity != null)
		{
			RemovePlayerIden(conn.identity);
		}
		// hostPlayer.ServerRemovePlayer(conn.identity);
	}
	public override void OnStartClient()
	{
		base.OnStartClient();
	}
	public override void OnClientConnect()
	{
		base.OnClientConnect();
	}
	public override void OnClientDisconnect()
	{
		base.OnClientDisconnect();
	}
	private IEnumerator WaitAndRegisterPlayer(NetworkConnectionToClient conn)
	{
		// Wait until playerListSync exists in scene
		while (hostPlayer == null)
		{
			hostPlayer = FindObjectOfType<PlayerObject>();
			yield return null;
		}

		hostPlayer.ServerAddPlayer(conn.identity);
	}

	public void AddPlayerIden(NetworkIdentity netIden)
	{
		listOfNetworkIden.Add(netIden);
	}
	public void RemovePlayerIden(NetworkIdentity netIden)
	{
		listOfNetworkIden.Remove(netIden);
	}

	#region GetData
	public NetworkIdentity[] GetNetworkIdentities()
	{
		return listOfNetworkIden.ToArray();
	}
	public NetworkIdentity GetNetworkIdentityById(int id)
	{
		return listOfNetworkIden.Find(x => x.netId == id);
	}
	public NetworkIdentity GetLatestId()
	{
		return listOfNetworkIden.Find(x => x.netId == listOfNetworkIden.Count);
	}
	public NetworkIdentity GetPlayerByNetworkIden(uint _netId)
	{
		foreach (var player in listOfNetworkIden)
		{
			if (player.netId == _netId)
			{
				return player;
			}
		}
		return null;
	}
	#endregion
}
