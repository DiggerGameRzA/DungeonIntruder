using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Singleton<NetworkManager>
{
    // public StartGameResult result;
    public Player localPlayer;
    [SerializeField] private GameObject _playerPrefab;
    // [SerializeField] private Dictionary<PlayerRef, NetworkObject> spawnedCharacters = new();
    
    // public NetworkRunner runner;

    // public async void StartGame(GameMode mode)
    // {
    //     // Create the Fusion runner and let it know that we will be providing user input
    //     runner = gameObject.AddComponent<NetworkRunner>();
    //     runner.ProvideInput = true;

    //     // Create the NetworkSceneInfo from the current scene
    //     var scene = SceneRef.FromIndex(0);
    //     var sceneInfo = new NetworkSceneInfo();
    //     if (scene.IsValid) {
    //         sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
    //     }

    //     // Start or join (depends on gamemode) a session with a specific name
    //     result = await runner.StartGame(new StartGameArgs()
    //     {
    //         GameMode = mode,
    //         SessionName = "TestRoom",
    //         Scene = scene,
    //         SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
    //         ObjectProvider = new MyObjectProvider()
    //     });
    // }
    
    // public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    // {
        
    // }

    // public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    // {
        
    // }

    // public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    // {
    //     NetworkObject networkPlayerObject = new NetworkObject();
    //     if (runner.IsServer)
    //     {
    //         // Create a unique position for the player
    //         Vector3 spawnPosition = 
    //             new Vector3(((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 2) - 5, 0, 0);
    //         // Vector3 spawnPosition = Vector3.zero;
    //         networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
    //         // Keep track of the player avatars for easy access
    //         spawnedCharacters.Add(player, networkPlayerObject);
    //     }
    // }

    // public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    // {
    //     if (spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
    //     {
    //         runner.Despawn(networkObject);
    //         spawnedCharacters.Remove(player);
    //     }
    // }

    // public void OnInput(NetworkRunner runner, NetworkInput input)
    // {
    //     Vector2 right = Vector2.right;
    //     Vector2 up = Vector2.up;
    //     Vector2 dir = right * InputManager.GetHorInput() + up * InputManager.GetVerInput();

    //     // Vector2 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     NetworkInputData data = new NetworkInputData();
    //     data.direction += dir;
    //     // data.mousePos = dif;

    //     input.Set(data);
    // }

    // public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    // {
        
    // }

    // public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    // {
        
    // }

    // public void OnConnectedToServer(NetworkRunner runner)
    // {
        
    // }

    // public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    // {
        
    // }

    // public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    // {
        
    // }

    // public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    // {
        
    // }

    // public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    // {
        
    // }

    // public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    // {
        
    // }

    // public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    // {
        
    // }

    // public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    // {
        
    // }

    // public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    // {
        
    // }

    // public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    // {
        
    // }

    // public void OnSceneLoadDone(NetworkRunner runner)
    // {
        
    // }

    // public void OnSceneLoadStart(NetworkRunner runner)
    // {
        
    // }
}

// public struct NetworkInputData : INetworkInput
// {
//     public Vector2 direction;
//     public Vector2 mousePos;
// }

// public class MyObjectProvider : NetworkObjectProviderDefault
// {
    
// }

