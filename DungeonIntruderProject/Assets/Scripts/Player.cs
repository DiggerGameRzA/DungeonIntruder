using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IPlayer
{
    Rigidbody2D rb;
    IMovement movement;
    Stats stats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        movement = new Movement(this);
    }

    void FixedUpdate()
    {
        if (!InputManager.evade)
        {
            movement.Run();
        }
    }
    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
    public Stats GetStats()
    {
        return stats;
    }
    public IMovement GetMovement()
    {
        return movement;
    }
}
