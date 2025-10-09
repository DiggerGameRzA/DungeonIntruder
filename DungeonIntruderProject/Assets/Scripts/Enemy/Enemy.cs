using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class Enemy : MonoBehaviour
{
    // private PlayerObject _player;
    private NavMeshAgent agent;
    [SerializeField] public Health health;
    [SerializeField] public float speed;
    [SerializeField] public float detectRange;
    // [SerializeField] private SpriteRenderer sprite;
    // [SerializeField] private float delayFireTime;
    // [SerializeField] private float tempTime;
    // [SerializeField] private GameObject prefabBullet;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        // tempTime = delayFireTime;
        agent.speed = speed;
    }

    void Update()
    {
        PlayerObject[] players = FindObjectsOfType<PlayerObject>();

        if (players.Length > 0)
        {
            PlayerObject closestPlayer = null;
            float minDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (PlayerObject player in players)
            {
                float distance = Vector3.Distance(currentPosition, player.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = player;
                }
            }

            if (Vector3.Distance(closestPlayer.transform.position, transform.position) <= detectRange)
            {
                agent.SetDestination(closestPlayer.transform.position);
            }
        }
        // Vector3 playerPos = transform.position - _player.transform.position;
        // if (playerPos.x < 0)
        // {
        //     sprite.flipX = false;
        // }
        // else
        // {
        //     sprite.flipX = true;
        // }

        // if (tempTime <= 0f)
        // {
        //     Fire();
        //     tempTime = delayFireTime;
        // }
        // else
        // {
        //     tempTime -= Time.deltaTime;
        // }
    }

    // private void Fire()
    // {
    //     Vector2 playerDir = _player.transform.position - transform.position;
    //     Bullet bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
    //     bullet.isFromEnemy = true;
    //     bullet.GetComponent<Rigidbody2D>().velocity = (playerDir).normalized * 5f;
    // }
}
