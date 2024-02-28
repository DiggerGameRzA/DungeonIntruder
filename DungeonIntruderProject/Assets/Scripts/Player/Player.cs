using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Player : MonoBehaviour
{
    // CharacterController controller;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;
    public PlayerState State;
    public PlayerMovement movement;
    [SerializeField] Stats stats;
    [SerializeField] public Transform gunPos;
    [SerializeField] public Transform bulletPos;

    [SerializeField] public ParticleSystem healParticle;

    private RewardObject rewardObj = null;

    void Start()
    {
        // controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        movement = new PlayerMovement(this);
        State = PlayerState.Combat;
        
        WeaponManager.Instance.EquipGun();
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<InputManager>().canMove)
        {
            movement.Run();
        }
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Combat:
                FindObjectOfType<UIManager>().spellUICtrl.EnableUI(false);
                FindObjectOfType<InputManager>().SetCanMove(true);
                FindObjectOfType<InputManager>().SetCanEvade(true);
                FindObjectOfType<InputManager>().ObtainReward(rewardObj);
                break;
            case PlayerState.Casting:
                FindObjectOfType<UIManager>().spellUICtrl.EnableUI(true);
                FindObjectOfType<InputManager>().SetCanMove(false);
                FindObjectOfType<InputManager>().SetCanEvade(false);
                movement.StopRun();
                break;
            case PlayerState.StandBy:
                break;
            case PlayerState.Evading:
                FindObjectOfType<InputManager>().SetCanMove(false);
                FindObjectOfType<InputManager>().SetCanEvade(false);
                StartCoroutine(OnIFraming(0f, 0.4f));
                SwitchState(PlayerState.Combat, 0.2f);
                break;
            default:
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            // FindObjectOfType<InputManager>().CollectItem(col.GetComponent<Item>());
        }
        else if (col.CompareTag("Reward"))
        {
            rewardObj = col.GetComponent<RewardObject>();
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            
        }
        else if (col.CompareTag("Reward"))
        {
            rewardObj = null;
        }
    }

    public void SwitchState(PlayerState state, float delay = 0f)
    {
        StartCoroutine(OnDelaySwitchState(state, delay));
    }

    private IEnumerator OnDelaySwitchState(PlayerState state, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        State = state;
    }

    // public CharacterController GetController()
    // {
    //     return controller;
    // }

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    public Stats GetStats()
    {
        return stats;
    }

    public PlayerMovement GetMovement()
    {
        return movement;
    }

    public void FlipPlayerSprite(bool flip)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = flip;
    }

    public IEnumerator OnIFraming(float onBegin, float onEnding)
    {
        yield return new WaitForSeconds(onBegin);
        collider.enabled = false;

        yield return new WaitForSeconds(onEnding);
        collider.enabled = true;
    }

    public float GetTrueMaxHP()
    {
        return stats.maxHP + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxHp);
    }
    public float GetTrueMoveSpeed()
    {
        return stats.movementSpeed * (1 + (AugmentInventory.Instance.GetAugmentValue(AugmentType.MoveSpeed)) / 100f );
    }
    public float GetTrueMaxMana()
    {
        return stats.maxMana + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxMana);
    }

    public void RestoreMana(float mana)
    {
        stats.currentMana += mana;
        if (stats.currentMana >= GetTrueMaxMana())
        {
            stats.currentMana = GetTrueMaxMana();
        }
        FindObjectOfType<UIManager>().UpdateMana();
    }
    public void CostMana(float mana)
    {
        stats.currentMana -= mana;
        if (stats.currentMana <= 0)
        {
            stats.currentMana = 0;
        }
        FindObjectOfType<UIManager>().UpdateMana();
    }
    
    public void TakeHeal(float heal)
    {
        stats.currentHP += heal;
        if (stats.currentHP >= GetTrueMaxHP())
        {
            stats.currentHP = GetTrueMaxHP();
        }
        FindObjectOfType<UIManager>().UpdateHP();
    }
    
    public void TakeDamage(float dmg)
    {
        stats.currentHP -= dmg;
        if (stats.currentHP <= 0)
        {
            stats.currentHP = 0;
            OnDead();
        }
        FindObjectOfType<UIManager>().UpdateHP();
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
    }
}
public enum PlayerState
{
    StandBy,
    Combat,
    Evading,
    Casting
}
