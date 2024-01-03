using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : Singleton<GunInventory>
{
    [SerializeField] public List<GunStats> gSlots;
    public int currentSlot;
    int slotIndex = 2;
    private void Start()
    {
        if (gSlots.Count != 0)
        {
            currentSlot = 0;
            WeaponManager.instance.currentGun = gSlots[currentSlot];
            ShowGun(WeaponManager.instance.currentGun.Name);
        }
        for (int i = 0; i < gSlots.Count - slotIndex; i++)
            gSlots.Add(null);
    }
    public void AddWeapon(IInventoryItem item)
    {
        int empty = FindEmptySlot();
        gSlots[empty] = (GunStats)item.GetIGunStats();
        currentSlot = empty;

        WeaponManager.instance.currentGun = item.GetIGunStats();
        ShowGun(WeaponManager.instance.currentGun.Name);
        FindObjectOfType<UIManager>().PickUpText(item);
        item.OnPickUp();
    }
    int FindEmptySlot()
    {
        for (int i = 0; i < gSlots.Count; i++)
        {
            if (gSlots[i] == null)
                return i;
        }
        return currentSlot;
    }
    public void ShowGun(string name)
    {
        Player player = FindObjectOfType<Player>();
        Transform guns = player.GetTransform().GetChild(1).GetChild(0);
        foreach (Transform i in guns)
        {
            i.gameObject.SetActive(true);
            if (i.name != name)
                i.gameObject.SetActive(false);
        }
    }
}
