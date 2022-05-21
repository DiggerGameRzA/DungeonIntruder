using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    IPlayer player;
    Stats stats;
    CharacterController controller;
    Transform transform;
    public PlayerMovement(IPlayer player)
    {
        this.player = player;
        controller = player.GetController();
        transform = player.GetTransform();
        stats = player.GetStats();
    }
    public void Run()
    {
        //rb.AddForce(GetMovementDir() * player.GetStats().runSpeed);
        controller.Move(GetMovementDir() * stats.movementSpeed * Time.deltaTime);
    }
    public IEnumerator Evade()
    {
        if (GetMovementDir().normalized != Vector2.zero)
        {
            InputManager.instance.canMove = false;
            InputManager.instance.tempEvadeTime = stats.evadeCooldown;

            Vector3 startPos = transform.position;

            while (stats.evadeDistance > Vector3.Distance(startPos, transform.position))
            {
                controller.Move(GetMovementDir().normalized * stats.evadeSpeed * Time.deltaTime);
                yield return null;
            }
            InputManager.instance.canMove = true;
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
