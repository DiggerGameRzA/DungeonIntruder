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
        // RelayManager.Instance.CreateRelay();
        relayNetworkManager.StartRelayHost(4);
        groupLobby.SetActive(false);
    }
    public void OnClickedJoin()
    {
        // RelayManager.Instance.JoinRelay(inputCode.text);
        relayNetworkManager.relayJoinCode = inputCode.text;
        relayNetworkManager.JoinRelayServer();
    }
    public void OnClickedGetCode()
    {
        // textHostCode.text = $"Code = {RelayManager.Instance.joinCode}";
        textHostCode.text = $"Code = {relayNetworkManager.relayJoinCode}";
    }
}
