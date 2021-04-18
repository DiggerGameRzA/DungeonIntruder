using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IMovement
{
    IPlayer player;
    Rigidbody2D rb;
    public Movement(IPlayer player)
    {
        this.player = player;
        rb = this.player.GetRigidbody2D();
    }
    public void Run()
    {
        rb.AddForce(GetMovementDir() * player.GetStats().runSpeed);
    }
    public void Evade()
    {
        rb.velocity = GetMovementDir() * player.GetStats().evadeDistance;
    }
    public Vector2 GetMovementDir()
    {
        Vector2 right = Vector2.right;
        Vector2 up = Vector2.up;
        return right * InputManager.GetHorInput() + up * InputManager.GetVerInput();
    }
    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
}
