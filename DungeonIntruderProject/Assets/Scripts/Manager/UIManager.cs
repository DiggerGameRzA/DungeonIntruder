using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;
    public Text gunText;
    public Text slotIndex;
    public Text hpText;
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
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

    public void UpdateHP()
    {
        hpText.text = $"HP: {player.GetStats().currentHP}/{player.GetStats().maxHP}";
    }
}
