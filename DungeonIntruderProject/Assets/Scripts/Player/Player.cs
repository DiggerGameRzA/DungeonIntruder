using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Player : NetworkBehaviour
{
    // CharacterController controller;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;
    public PlayerState State;
    private PlayerMovement movement;
    Stats stats;
    public Transform gunPos;
    public Transform bulletPos;

    public ParticleSystem healParticle;

    private RewardObject rewardObj = null;
    private PortalObject portalObj = null;

    private bool isLoaded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        movement.SetInfos(this);
        State = PlayerState.Combat;

        CinemachineTargetGroup[] targetGroups = FindObjectsOfType<CinemachineTargetGroup>();
        foreach (var target in targetGroups)
        {
            if (target.name == "Target Group")
                target.AddMember(transform, 1f, 0f);
        }

        StartCoroutine(WaitForGameStart());
    }

    private void FixedUpdate()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (InputManager.Instance.canMove)
        {
            GetInput(out NetworkInputData data);
            if (movement == null)
            {
                movement = gameObject.AddComponent<PlayerMovement>();
                movement.SetInfos(this);
            }

            data.direction.Normalize();
            if (isLoaded)
                rb.velocity = data.direction.normalized * GetTrueMoveSpeed();
        }
    }

    IEnumerator WaitForGameStart()
    {
        yield return new WaitUntil(() => NetworkManager.Instance.result != null);
        isLoaded = true;
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Combat:
                if (UIManager.Instance.spellUICtrl != null)
                    UIManager.Instance.spellUICtrl.EnableUI(false);
                InputManager.Instance.SetCanMove(true);
                InputManager.Instance.SetCanEvade(true);
                InputManager.Instance.ObtainReward(rewardObj);
                InputManager.Instance.EnterPortal(portalObj);
                break;
            case PlayerState.Casting:
                if (UIManager.Instance.spellUICtrl != null)
                    UIManager.Instance.spellUICtrl.EnableUI(true);
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
            // FindObjectOfType<InputManager>().CollectItem(col.GetComponent<Item>());
        }
        else if (col.CompareTag("Reward"))
        {
            rewardObj = col.GetComponent<RewardObject>();
            portalObj = col.GetComponent<PortalObject>();
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
            portalObj = null;
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
        if (AugmentInventory.Instance == null)
            return stats.maxHP;
        return stats.maxHP + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxHp);
    }
    public float GetTrueMoveSpeed()
    {
        if (stats == null)
            stats = GetComponent<Stats>();
        if (AugmentInventory.Instance == null)
            return stats.movementSpeed;
        return stats.movementSpeed * (1 + (AugmentInventory.Instance.GetAugmentValue(AugmentType.MoveSpeed)) / 100f );
    }
    public float GetTrueMaxMana()
    {
        if (AugmentInventory.Instance == null)
            return stats.maxMana;
        return stats.maxMana + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxMana);
    }

    public void RestoreMana(float mana)
    {
        stats.currentMana += mana;
        if (stats.currentMana >= GetTrueMaxMana())
        {
            stats.currentMana = GetTrueMaxMana();
        }
        UIManager.Instance.UpdateMana();
    }
    public void CostMana(float mana)
    {
        stats.currentMana -= mana;
        if (stats.currentMana <= 0)
        {
            stats.currentMana = 0;
        }
        UIManager.Instance.UpdateMana();
    }
    
    public void TakeHeal(float heal)
    {
        stats.currentHP += heal;
        if (stats.currentHP >= GetTrueMaxHP())
        {
            stats.currentHP = GetTrueMaxHP();
        }
        UIManager.Instance.UpdateHP();
    }
    
    public void TakeDamage(float dmg)
    {
        stats.currentHP -= dmg;
        if (stats.currentHP <= 0)
        {
            stats.currentHP = 0;
            OnDead();
        }
        UIManager.Instance.UpdateHP();
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
