using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [ProButton] public void HostGame()
    {
        // NetworkManager.Instance.StartGame(GameMode.Host);
    }
    [ProButton] public void JoinGame()
    {
        // NetworkManager.Instance.StartGame(GameMode.Client);
    }
}
