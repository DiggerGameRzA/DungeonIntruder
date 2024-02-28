using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string _name = "";
    [SerializeField] Sprite _image = null;


    public string Name { get { return _name; } }
    public Sprite Image { get { return _image; } }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI(bool show)
    {
        GameObject go = transform.GetChild(1).gameObject;
        go.SetActive(show);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ShowUI(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ShowUI(false);
        }
    }
    public GunStats GetGunStats()
    {
        GunStats gunStats = null;
        if (GetComponent<GunStats>() != null)
        {
            gunStats = GetComponent<GunStats>();
        }
        return gunStats;
    }
}
