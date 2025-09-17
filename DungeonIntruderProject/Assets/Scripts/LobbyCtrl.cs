using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCtrl : MonoBehaviour
{
    [SerializeField] private GameObject groupLobby;
    public void OnClickedHost()
    {
        Mirror.NetworkManager.singleton.StartHost();
        groupLobby.SetActive(false);
    }
}
