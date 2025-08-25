using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] Player player;

    [SerializeField] public bool canEvade { get; private set; }
    [SerializeField] public bool canMove { get; private set; }

    private float tempFireTime = 0;
    [HideInInspector] public float tempEvadeTime = 0f;
    float mouseRotZ = 0;
    void Start()
    {
        canMove = true;
        canEvade = true;
        if (NetworkManager.Instance.localPlayer != null)
            player = NetworkManager.Instance.localPlayer;
    }

    void Update()
    {
        if (player == null)
        {
            if (NetworkManager.Instance.localPlayer != null)
                player = NetworkManager.Instance.localPlayer;
            else
                return;
        }
        // player.RPC_SendMouseRot(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (tempEvadeTime > 0)
            tempEvadeTime -= Time.deltaTime;
        if (tempFireTime > 0)
            tempFireTime -= Time.deltaTime;

        
        
        if (player.State == PlayerState.Combat)
        {
            if (tempEvadeTime <= 0)
            {
                canEvade = true;
                if (Input.GetButtonDown("Evade"))
                    StartCoroutine(player.GetMovement().Evade());
            }
            else
            {
                canEvade = false;
            }
            
            if (Input.GetButton("Fire"))
            {
                SwitchGun(0);
                if (GunInventory.Instance.gSlots[0] == null)
                    return;
            }
            if (Input.GetButton("Fire2"))
            {
                SwitchGun(1);
                if (GunInventory.Instance.gSlots[1] == null)
                    return;
            }
            
            if (WeaponManager.Instance.currentGun != null)
            {
                GunStats gun = WeaponManager.Instance.currentGun;
                if (gun.Pattern == GunPattern.Auto || gun.Pattern == GunPattern.Beam)
                {
                    if (Input.GetButton("Fire") || Input.GetButton("Fire2"))
                    {
                        if (tempFireTime <= 0)
                        {
                            if (gun.Pattern == GunPattern.Auto)
                            {
                                WeaponManager.Instance.Fire();
                            }
                            else if (gun.Pattern == GunPattern.Beam)
                            {
                                WeaponManager.Instance.FireBeam();
                            }

                            // player.GetStats().currentAmmo -= cost;
                            FindObjectOfType<UIManager>().UpdateAmmo();
                            float fireRate = gun.FireRate + (gun.ModifierInfo.fireRatePercentage / 100f * gun.FireRate);
                            tempFireTime = 1 / fireRate;
                        }
                    }
                    else
                    {
                        WeaponManager.Instance.DestroyBeam();
                    }
                }
                else if (gun.Pattern == GunPattern.Single || gun.Pattern == GunPattern.Burst)
                {
                    if (Input.GetButtonDown("Fire") || Input.GetButtonDown("Fire2"))
                    {
                        if (tempFireTime <= 0)
                        {
                            if (gun.Pattern == GunPattern.Single)
                            {
                                WeaponManager.Instance.Fire();
                            }
                            else if (gun.Pattern == GunPattern.Burst)
                            {
                                WeaponManager.Instance.FireBurst();
                            }

                            // player.GetStats().currentAmmo -= cost;
                            FindObjectOfType<UIManager>().UpdateAmmo();
                            float fireRate = gun.FireRate + (gun.ModifierInfo.fireRatePercentage / 100f * gun.FireRate);
                            tempFireTime = 1 / fireRate;
                        }
                    }
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

        // if(Input.GetAxis("Mouse ScrollWheel") > 0f) //up
        // {
        //     FindNextGun(true);
        // }
        // else if(Input.GetAxis("Mouse ScrollWheel") < 0f) //down
        // {
        //     FindNextGun(false);
        // }

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
    public void EnterPortal(PortalObject portalObject)
    {
        if (portalObject == null) return;
        
        if (Input.GetButtonDown("Collect"))
        {
            portalObject.OnInteractPortal();
        }
    }
    
    public void CollectItem(Item item)
    {
        if (Input.GetButtonDown("Collect"))
        {
            // GunInventory.Instance.AddWeapon(item);
        }
    }
    
    void SwitchGun(int slot)
    {
        GunInventory inventory = GunInventory.Instance;

        if (inventory.currentSlot == slot)
            return;
        if (inventory.gSlots.Count <= slot)
            return;

        if (inventory.gSlots[slot] != null)
        {
            inventory.currentSlot = slot;
            WeaponManager.Instance.currentGun = inventory.gSlots[inventory.currentSlot];
            WeaponManager.Instance.EquipGun();
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
    public float GetMousePosition(Transform player, Vector3 mousePos)
    {
        Vector3 dif = mousePos - player.position;
        dif.Normalize();
        mouseRotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

        return mouseRotZ;
    }
}
