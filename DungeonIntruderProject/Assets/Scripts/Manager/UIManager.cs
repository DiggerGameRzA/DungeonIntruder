using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;
    public Text gunText;
    public Text slotIndex;
    IPlayer player;
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<IPlayer>();
        // ammoText.text = FindObjectOfType<Player>().GetComponent<Stats>().currentAmmo.ToString();
    }

    void Update()
    {
        slotIndex.text = "slot " + GunInventory.instance.currentSlot;
    }
    public void UpdateAmmo()
    {
        // ammoText.text = player.GetStats().currentAmmo.ToString();
    }
    public void PickUpText(IInventoryItem item)
    {
        gunText.text = "You got " + item.GetIGunStats().ModifierInfo.name + " " + item.Name;
    }
}
