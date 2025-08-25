using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalObject : MonoBehaviour
{
    [SerializeField] private int scene;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private int ready = 0;
    private void Start()
    {
        interactUI.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactUI.SetActive(false);
        }
    }

    public void OnInteractPortal()
    {
        // SceneManager.LoadScene(scene);

        ready++;
        // UIManager.Instance.UpdateReady(ready, NetworkManager.Instance.runner.ActivePlayers.Count());
        // StartCoroutine(OnWaitForReady());
        GetComponent<Collider2D>().enabled = false;
    }

    // private IEnumerator OnWaitForReady()
    // {
    //     yield return new WaitUntil(() => ready >= NetworkManager.Instance.runner.ActivePlayers.Count());
    //     Debug.Log("Yee");
    // }
}
