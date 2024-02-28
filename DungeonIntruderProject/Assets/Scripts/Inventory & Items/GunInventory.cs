using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : Singleton<GunInventory>
{
    [SerializeField] public List<GunStats> gSlots = new List<GunStats>();
    public int currentSlot;
    int slotIndex = 2;
    private void Start()
    {
        if (gSlots.Count != 0)
        {
            currentSlot = 0;
            WeaponManager.Instance.currentGun = gSlots[currentSlot];
            // ShowGun(WeaponManager.Instance.currentGun.GunName);
        }

        while (gSlots.Count < slotIndex)
        {
            gSlots.Add(null);
        }
    }
    public void AddWeapon(GunStats gunStats)
    {
        int empty = FindEmptySlot();
        gSlots[empty] = gunStats;
        currentSlot = empty;

        WeaponManager.Instance.currentGun = gunStats;
        // WeaponManager.Instance.EquipGun();
        // FindObjectOfType<UIManager>().PickUpText(gunStats);
        // item.OnPickUp();
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

    public bool IsInventoryFull()
    {
        GunStats slot = gSlots[FindEmptySlot()];

        return slot != null;
    }
}
