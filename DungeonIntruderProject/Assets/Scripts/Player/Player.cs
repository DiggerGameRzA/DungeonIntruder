using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EquipmentInventory))]
public class Player : MonoBehaviour
{
    // CharacterController controller;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;
    public PlayerState State;
    public PlayerMovement movement;
    [SerializeField] Stats stats;
    [SerializeField] AugmentInventory augmentInv;

    [SerializeField] private ParticleSystem healParticle;

    private RewardObject rewardObj = null;

    void Start()
    {
        // controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        augmentInv = GetComponent<AugmentInventory>();
        movement = new PlayerMovement(this);
        State = PlayerState.Combat;
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance.canMove)
        {
            movement.Run();
        }
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Combat:
                SpellManager.Instance.EnableUI(false);
                InputManager.Instance.SetCanMove(true);
                InputManager.Instance.SetCanEvade(true);
                InputManager.Instance.ObtainReward(rewardObj);
                break;
            case PlayerState.Casting:
                SpellManager.Instance.EnableUI(true);
                InputManager.Instance.SetCanMove(false);
                InputManager.Instance.SetCanEvade(false);
                movement.StopRun();
                break;
            case PlayerState.StandBy:
                break;
            case PlayerState.Evading:
                InputManager.Instance.SetCanMove(false);
                InputManager.Instance.SetCanEvade(false);
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
            InputManager.Instance.CollectItem(col.GetComponent<IInventoryItem>());
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
    
    public AugmentInventory GetAugmentInv()
    {
        return augmentInv;
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
        return stats.maxHP + augmentInv.GetAugmentValue(AugmentType.MaxHp);
    }
    public float GetTrueMoveSpeed()
    {
        return stats.movementSpeed * (1 + (augmentInv.GetAugmentValue(AugmentType.MoveSpeed)) / 100f );
    }
    public float GetTrueMaxMana()
    {
        return stats.maxMana + augmentInv.GetAugmentValue(AugmentType.MaxMana);
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
        healParticle.gameObject.SetActive(true);
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
