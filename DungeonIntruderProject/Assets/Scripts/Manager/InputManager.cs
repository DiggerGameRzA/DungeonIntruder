﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    Player player;
    Hand hand;

    [SerializeField] public bool canEvade { get; private set; }
    [SerializeField] public bool canMove { get; private set; }

    [SerializeField] float tempFireTime = 0;
    public float tempEvadeTime = 0f;
    float mouseRotZ = 0;
    void Start()
    {
        canMove = true;
        canEvade = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hand = FindObjectOfType<Hand>();
    }

    void Update()
    {
        if (player == null)
            return;
        if (!player.gameObject.activeInHierarchy)
            return;
        
        tempEvadeTime -= Time.deltaTime;
        tempFireTime -= Time.deltaTime;

        
        
        if (player.State == PlayerState.Combat)
        {
            if (tempEvadeTime <= 0)
            {
                canEvade = true;
                if (Input.GetButtonDown("Evade"))
                    StartCoroutine(player.movement.Evade());
            }
            else
            {
                canEvade = false;
            }
            
            if (Input.GetButton("Fire") && WeaponManager.Instance.currentGun != null)
            {
                IGunStats gun = WeaponManager.Instance.currentGun;
                float cost = gun.Cost + gun.ModifierInfo.ammoCost;
                if (cost < 0)
                    cost = 0;
                if (tempFireTime <= 0)
                {
                    WeaponManager.Instance.Fire();

                    // player.GetStats().currentAmmo -= cost;
                    FindObjectOfType<UIManager>().UpdateAmmo();
                    float fireRate = gun.FireRate + (gun.ModifierInfo.fireRatePercentage / 100f * gun.FireRate);
                    tempFireTime = 1 / fireRate;
                }
            }
            
            if (Input.GetButtonDown("Cast Spell"))
            {
                SpellManager.Instance.ClearInputList();
                player.SwitchState(PlayerState.Casting);
            }
        }
        else if (player.State == PlayerState.Casting)
        {
            if (Input.GetButtonDown("Evade"))
            {
                player.SwitchState(PlayerState.Combat);
                
                SpellManager.Instance.ConfirmCastSpell();
            }

            if (Input.GetButtonDown("Cast Spell"))
            {
                SpellManager.Instance.ClearInputList();
                player.SwitchState(PlayerState.Combat);
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f) //up
        {
            FindNextGun(true);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f) //down
        {
            FindNextGun(false);
        }

        if (Input.GetButtonDown("Drop"))
        {
            WeaponManager.Instance.DropGun();
        }
    }

    public void SetCanMove(bool isEnable, float delay = 0f)
    {
        StartCoroutine(OnDelaySetCanMove(isEnable, delay));
    }
    public void SetCanEvade(bool isEnable, float delay = 0f)
    {
        StartCoroutine(OnDelaySetCanEvade(isEnable, delay));
    }

    IEnumerator OnDelaySetCanMove(bool isEnable, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        canMove = isEnable;
    }
    
    IEnumerator OnDelaySetCanEvade(bool isEnable, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        canEvade = isEnable;
    }
    
    public void ObtainReward(RewardObject rewardObject)
    {
        if (rewardObject == null) return;
        
        if (Input.GetButtonDown("Collect"))
        {
            rewardObject.EnableRewardsUI();
        }
    }
    
    public void CollectItem(IInventoryItem item)
    {
        if (Input.GetButtonDown("Collect"))
        {
            GunInventory.Instance.AddWeapon(item);
        }
    }
    void FindNextGun(bool up)
    {
        GunInventory inventory = GunInventory.Instance;
        int newSlot = 0;
        if (up)
        {
            newSlot = inventory.currentSlot - 1;
            if (newSlot < 0)
                newSlot = inventory.gSlots.Count - 1;
        }
        else
        {
            newSlot = inventory.currentSlot + 1;
            if (newSlot > inventory.gSlots.Count - 1)
                newSlot = 0;
        }

        //print(inventory.currentSlot + " -> " + newSlot);
        if (inventory.gSlots[newSlot] != null)
        {
            inventory.currentSlot = newSlot;
            WeaponManager.Instance.currentGun = inventory.gSlots[inventory.currentSlot];
            WeaponManager.Instance.EquipGun();
        }
        else
            print("There is no others gun.");
        
    }
    public static float GetVerInput()
    {
        return Input.GetAxis("Vertical");
    }
    public static float GetHorInput()
    {
        return Input.GetAxis("Horizontal");
    }
    public float GetMousePosition(Transform player)
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        dif.Normalize();
        mouseRotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

        return mouseRotZ;
    }
}
