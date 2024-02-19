using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    Player player;
    Stats stats;
    // CharacterController controller;
    Rigidbody2D rb;
    Transform transform;
    public PlayerMovement(Player player)
    {
        this.player = player;
        // controller = player.GetController();
        rb = player.GetRigidBody();
        transform = player.GetTransform();
        stats = player.GetStats();
    }
    public void Run()
    {
        rb.velocity = GetMovementDir().normalized * player.GetStats().movementSpeed;
        // controller.Move(GetMovementDir() * stats.movementSpeed * Time.deltaTime);
    }
    public IEnumerator Evade()
    {
        if (GetMovementDir().normalized != Vector2.zero)
        {
            InputManager.instance.SetCanMove(false);
            InputManager.instance.tempEvadeTime = stats.evadeCooldown;

            Vector3 startPos = transform.position;

            while (stats.evadeDistance > Vector3.Distance(startPos, transform.position))
            {
                // controller.Move(GetMovementDir().normalized * stats.evadeSpeed * Time.deltaTime);
                rb.velocity = GetMovementDir().normalized * stats.evadeSpeed;
                yield return null;
            }
            InputManager.instance.SetCanMove(true);
        }
    }
    public Vector2 GetMovementDir()
    {
        Vector2 right = Vector2.right;
        Vector2 up = Vector2.up;
        Vector2 dir = right * InputManager.GetHorInput() + up * InputManager.GetVerInput();
        return dir;
    }
}
