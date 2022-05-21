using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    IPlayer player;
    Hand hand;

    public bool canEvade = false;
    public bool canMove = true;

    [SerializeField] float tempFireTime = 0;
    public float tempEvadeTime = 0f;
    float mouseRotZ = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPlayer>();
        hand = FindObjectOfType<Hand>();
    }

    void Update()
    {
        tempEvadeTime -= Time.deltaTime;
        tempFireTime -= Time.deltaTime;

        if (mouseRotZ > 90 || mouseRotZ < -90)
        {
            hand.FlipGun(true);
            player.FlipPlayerSprite(true);
        }
        else
        {
            hand.FlipGun(false);
            player.FlipPlayerSprite(false);
        }

        if (tempEvadeTime <= 0)
        {
            canEvade = true;
            if (Input.GetButtonDown("Evade"))
                player.isEvade = true;
        }
        else
        {
            canEvade = false;
        }

        if (Input.GetButton("Fire") && WeaponManager.instance.currentGun != null)
        {
            IGunStats gun = WeaponManager.instance.currentGun;
            float cost = gun.Cost + gun.ModifierInfo.ammoCost;
            if (cost < 0)
                cost = 0;
            if (tempFireTime <= 0&& player.GetStats().currentAmmo >= cost)
            {
                WeaponManager.instance.Fire();

                player.GetStats().currentAmmo -= cost;
                FindObjectOfType<UIManager>().UpdateAmmo();
                float fireRate = gun.FireRate + (gun.ModifierInfo.fireRatePercentage / 100f * gun.FireRate);
                tempFireTime = 1 / fireRate;
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
            WeaponManager.instance.DropGun();
        }
    }
    public void CollectItem(IInventoryItem item)
    {
        if (Input.GetButtonDown("Collect"))
        {
            GunInventory.instance.AddWeapon(item);
        }
    }
    void FindNextGun(bool up)
    {
        GunInventory inventory = GunInventory.instance;
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
            WeaponManager.instance.currentGun = inventory.gSlots[inventory.currentSlot];
            WeaponManager.instance.EquipGun();
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
