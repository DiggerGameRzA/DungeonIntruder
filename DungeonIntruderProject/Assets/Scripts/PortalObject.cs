using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalObject : MonoBehaviour
{
    [SerializeField] private int scene;
    [SerializeField] private GameObject interactUI;
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

    public void OnEnterPortal()
    {
        SceneManager.LoadScene(scene);
    }
}
