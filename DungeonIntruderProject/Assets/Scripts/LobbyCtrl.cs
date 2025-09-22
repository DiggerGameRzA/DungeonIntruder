using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using Utp;

public class LobbyCtrl : MonoBehaviour
{
    [SerializeField] private GameObject groupLobby;
    [SerializeField] private TMP_InputField inputCode;
    [SerializeField] private TextMeshProUGUI textHostCode;

    private RelayNetworkManager relayNetworkManager;
    void Start()
    {
        relayNetworkManager = NetworkManager.singleton.GetComponent<RelayNetworkManager>();
    }
    public void OnClickedHost()
    {
        relayNetworkManager.StartRelayHost(4, callback:
        () =>
        {
            groupLobby.SetActive(false);

            // NetworkIdentity host = relayNetworkManager.GetNetworkIdentityById(1);
            // Debug.Log(host.connectionToClient);
            // GameManager.Instance.CmdSetSpawnPos();
        });
        
    }
    public void OnClickedJoin()
    {
        relayNetworkManager.JoinRelayServer(inputCode.text, 
        () =>
        {
            groupLobby.SetActive(false);

            // NetworkIdentity joinPlayer = relayNetworkManager.GetLatestId();
            // GameManager.Instance.CmdSetSpawnPos();
        });
    }
    public void OnClickedGetCode()
    {
        textHostCode.text = $"Code = {relayNetworkManager.relayJoinCode}";
    }
}
