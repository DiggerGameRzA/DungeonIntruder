﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    // public Modifier[] mods;
    // public GameObject[] guns;
    [SerializeField]
    public GunStats currentGun;
    [SerializeField] Transform bulletSpawn = null;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Fire()
    {
        bulletSpawn = FindObjectOfType<Player>().bulletPos;
        Bullet go = Instantiate(currentGun.Bullet, bulletSpawn.position, bulletSpawn.rotation);
        go.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * currentGun.Velocity;
    }
    public void EquipGun()
    {
        GunInventory.Instance.ShowGun(GunInventory.Instance.gSlots[GunInventory.Instance.currentSlot].Name);
    }
    public void DropGun()
    {
        GunInventory inv = GunInventory.Instance;
        if (inv.gSlots[inv.currentSlot] != null)
            inv.gSlots[inv.currentSlot] = null;
    }
}
