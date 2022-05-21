using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EquipmentInventory))]
public class Player : MonoBehaviour, IPlayer
{
    CharacterController controller;
    PlayerMovement movement;
    Stats stats;
    public bool isEvade { get; set; }
    void Start()
    {
        isEvade = false;
        controller = GetComponent<CharacterController>();
        stats = GetComponent<Stats>();
        movement = new PlayerMovement(this);
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
            isEvade = false;
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
}
