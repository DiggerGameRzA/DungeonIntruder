using System.Collections;
using System.Collections.Generic;
using Mirror;
using Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerObject player;
    Stats stats;
    // CharacterController controller;
    Rigidbody rb;
    public PlayerMovement(PlayerObject player)
    {
        this.player = player;
        // controller = player.GetController();
        rb = player.GetRigidBody();
        stats = player.GetStats();
    }

    public void SetInfos(PlayerObject player)
    {
        this.player = player;
        rb = player.GetRigidBody();
        stats = player.GetStats();
    }
    void Start()
    {
        player = GetComponent<PlayerObject>();
        if (player == null)
        {
            return;
        }

        rb = player.GetRigidBody();
        stats = player.GetStats();
    }
    void FixedUpdate()
    {
        if (player.isLocalPlayer)
        {
            Run(GetMovementDir());
        }
        // if (InputManager.Instance.canMove)
        // {
            // if (movement == null)
            // {
            //     movement = gameObject.AddComponent<PlayerMovement>();
            //     movement.SetInfos(this);
            // }

            // data.direction.Normalize();
            // rb.velocity = data.direction.normalized * GetTrueMoveSpeed();
        // }

        // if (isLoaded)
        //     hand.mouseRotZ = InputManager.Instance.GetMousePosition(transform, data.mousePos);
    }
    public void Run(Vector3 dir)
    {
        // rb.velocity = GetMovementDir().normalized * player.GetTrueMoveSpeed();
        // if (dir != Vector3.zero)
        //     rb.velocity = dir.normalized * player.GetTrueMoveSpeed();
        rb.AddForce(dir.normalized * player.GetTrueMoveSpeed(), ForceMode.Force);
    }

    public void StopRun()
    {
        rb.velocity = Vector2.zero;
    }
    public IEnumerator Evade()
    {
        if (GetMovementDir().normalized != Vector3.zero)
        {
            player.SwitchState(PlayerState.Evading);
            InputManager.Instance.tempEvadeTime = stats.evadeCooldown;

            Vector3 startPos = transform.position;

            while (stats.evadeDistance > Vector3.Distance(startPos, transform.position))
            {
                // controller.Move(GetMovementDir().normalized * stats.evadeSpeed * Time.deltaTime);
                rb.velocity = GetMovementDir().normalized * stats.evadeSpeed;
                yield return null;
            }
        }
    }
    public Vector3 GetMovementDir()
    {
        Vector3 right = Vector3.right;
        Vector3 forward = Vector3.forward;

        float _hor = Input.GetAxis("Horizontal");
        float _ver = Input.GetAxis("Vertical");
        // Debug.Log($"{_hor}, {_ver}");

        Vector3 dir = right * _hor + forward * _ver;
        return dir;
    }
}
