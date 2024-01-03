using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    Player player;
    Hand hand;

    [SerializeField] public bool canEvade;
    [SerializeField] public bool canMove;

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
        tempEvadeTime -= Time.deltaTime;
        tempFireTime -= Time.deltaTime;

        if (player.State == PlayerState.Combat)
        {
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
                if (tempFireTime <= 0)
                {
                    WeaponManager.instance.Fire();

                    // player.GetStats().currentAmmo -= cost;
                    FindObjectOfType<UIManager>().UpdateAmmo();
                    float fireRate = gun.FireRate + (gun.ModifierInfo.fireRatePercentage / 100f * gun.FireRate);
                    tempFireTime = 1 / fireRate;
                }
            }
            
            if (Input.GetButtonDown("Cast Spell"))
            {
                SpellManager.instance.ClearInputList();
                player.SwitchState(PlayerState.Casting);
            }
        }
        else if (player.State == PlayerState.Casting)
        {
            if (Input.GetButtonDown("Evade"))
            {
                player.SwitchState(PlayerState.Combat);
                
                SpellManager.instance.ConfirmCastSpell();
            }

            if (Input.GetButtonDown("Cast Spell"))
            {
                SpellManager.instance.ClearInputList();
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
            WeaponManager.instance.DropGun();
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
