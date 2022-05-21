using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public Modifier[] mods;
    public GameObject[] guns;
    [SerializeField]
    public IGunStats currentGun;
    [SerializeField] Transform bulletSpawn = null;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Fire()
    {
        GameObject go = Instantiate(currentGun.Bullet, bulletSpawn.position, bulletSpawn.rotation);
        go.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * currentGun.Velocity;
    }
    public void EquipGun()
    {
        GunInventory.instance.ShowGun(GunInventory.instance.gSlots[GunInventory.instance.currentSlot].Name);
    }
    public void DropGun()
    {
        GunInventory inv = GunInventory.instance;
        if (inv.gSlots[inv.currentSlot] != null)
            inv.gSlots[inv.currentSlot] = null;
    }
}
