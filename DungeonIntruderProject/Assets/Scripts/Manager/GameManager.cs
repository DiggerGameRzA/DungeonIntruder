using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    // [Command(requiresAuthority = false)]
    // public void CmdSetSpawnPos(NetworkConnectionToClient sender = null)
    // {
    //     sender.identity.gameObject.transform.position = new Vector3(-4 + sender.identity.netId, 1, 0);
    // }
}
