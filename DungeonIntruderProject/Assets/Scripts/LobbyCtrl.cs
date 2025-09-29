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
            ShowRoomCode();
        });
        
    }
    public void OnClickedJoin()
    {
        relayNetworkManager.JoinRelayServer(inputCode.text, 
        () =>
        {
            groupLobby.SetActive(false);
            ShowRoomCode();
        });
    }
    public void ShowRoomCode()
    {
        textHostCode.text = $"Code = {relayNetworkManager.relayJoinCode}";
    }
}
