using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EquipmentInventory))]
public class Player : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] private Collider2D collider;
    public PlayerState State;
    PlayerMovement movement;
    [SerializeField] Stats stats;

    [SerializeField] private ParticleSystem healParticle;
    public bool isEvade { get; set; }

    void Start()
    {
        isEvade = false;
        controller = GetComponent<CharacterController>();
        stats = GetComponent<Stats>();
        movement = new PlayerMovement(this);
        State = PlayerState.Combat;
    }

    private void Update()
    {
        if (InputManager.instance.canMove)
        {
            movement.Run();
        }

        if (isEvade)
        {
            StartCoroutine(movement.Evade());
            StartCoroutine(OnIFraming(0f, 0.4f));
            isEvade = false;
        }
    }

    public void SwitchState(PlayerState state)
    {
        State = state;
        if (State == PlayerState.Combat)
        {
            SpellManager.instance.EnableUI(false);
            InputManager.instance.SetCanMove(true);
            InputManager.instance.SetCanEvade(true, 0.3f);
        }
        
        if (State == PlayerState.Casting)
        {
            SpellManager.instance.EnableUI(true);
            InputManager.instance.SetCanMove(false);
            InputManager.instance.SetCanEvade(false);
        }
    }

    public CharacterController GetController()
    {
        return controller;
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

    public void TakeHeal(float heal)
    {
        stats.currentHP += heal;
        if (stats.currentHP >= stats.maxHP)
        {
            stats.currentHP = stats.maxHP;
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
        Destroy(this.gameObject);
    }
}
public enum PlayerState
{
    StandBy,
    Combat,
    Casting
}
