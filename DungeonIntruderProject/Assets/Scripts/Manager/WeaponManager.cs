using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    // public Modifier[] mods;
    // public GameObject[] guns;
    [SerializeField]
    public GunStats currentGun;
    [SerializeField] Transform bulletSpawn = null;

    private Bullet beam;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Fire()
    {
        // if (NetworkManager.Instance.localPlayer == null)
            return;
        // bulletSpawn = NetworkManager.Instance.localPlayer.bulletPos;
        // Bullet go = Instantiate(currentGun.Bullet, bulletSpawn.position, bulletSpawn.rotation);
        // go.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * currentGun.Velocity;
    }
    public void FireBurst()
    {
        StartCoroutine(OnFireBurst());
    }
    public void FireBeam()
    {
        if (beam != null)
            return;
        // if (NetworkManager.Instance.localPlayer == null)
            // return;
        
        // bulletSpawn = NetworkManager.Instance.localPlayer.bulletPos;
        beam = Instantiate(currentGun.Bullet, bulletSpawn);
        beam.transform.SetParent(bulletSpawn);
        // beam.GetComponent<Rigidbody2D>().velocity = bulletSpawn.right * currentGun.Velocity;
    }
    public void DestroyBeam()
    {
        if (beam == null)
            return;
        
        Destroy(beam.gameObject);
        beam = null;
    }

    private IEnumerator OnFireBurst()
    {
        // if (NetworkManager.Instance.localPlayer == null)
            // yield break;
        // bulletSpawn = NetworkManager.Instance.localPlayer.bulletPos;
        
        float _angle;
        if (bulletSpawn.right.x < 0)
        {
            _angle = 360 - (Mathf.Atan2(bulletSpawn.right.x, bulletSpawn.right.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            _angle = Mathf.Atan2(bulletSpawn.right.x, bulletSpawn.right.y) * Mathf.Rad2Deg;
        }
        float maxAngle = _angle + (currentGun.BurstRound * 5f);

        for (int i = 0; i < currentGun.BurstRound; i++)
        {
            float angle = _angle - ((maxAngle - _angle) / 2f) + (i * 5f);
            Vector2 rot = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
            
            Bullet go = Instantiate(currentGun.Bullet, bulletSpawn.position, bulletSpawn.rotation);
            go.GetComponent<Rigidbody2D>().velocity = rot * currentGun.Velocity;
            yield return new WaitForSeconds(currentGun.BurstDelay);
        }
    }
    
    public void EquipGun()
    {
        ShowGun(GunInventory.Instance.gSlots[GunInventory.Instance.currentSlot]);
    }
    
    public void ShowGun(GunStats gun)
    {
        PlayerObject player = FindObjectOfType<PlayerObject>();
        if (player == null)
            return;
        
        Transform guns = player.gunPos;
        SpriteRenderer gunSprite = guns.GetComponent<SpriteRenderer>();
        if (gun != null)
        {
            gunSprite.enabled = true;
            gunSprite.sprite = gun.Sprite.sprite;
            gunSprite.color = gun.Sprite.color;
        }
        else
        {
            gunSprite.enabled = false;
        }
    }
    public void DropGun()
    {
        GunInventory inv = GunInventory.Instance;
        if (inv.gSlots[inv.currentSlot] != null)
            inv.gSlots[inv.currentSlot] = null;
    }
}
