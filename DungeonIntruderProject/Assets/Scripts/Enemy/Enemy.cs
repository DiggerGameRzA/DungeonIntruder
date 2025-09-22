using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health health;
    [SerializeField] public float speed;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float delayFireTime;
    [SerializeField] private float tempTime;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private PlayerObject _player;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        tempTime = delayFireTime;
        agent.speed = speed;
    }

    void Update()
    {
        if (_player != null)
        {
            if (!_player.isActiveAndEnabled)
                return;
            
            if (Vector2.Distance(_player.transform.position, transform.position) > 10f)
                return;
            
            Vector3 playerPos = transform.position - _player.transform.position;
            if (playerPos.x < 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            
            if (tempTime <= 0f)
            {
                Fire();
                tempTime = delayFireTime;
            }
            else
            {
                tempTime -= Time.deltaTime;
            }

            agent.SetDestination(_player.transform.position);
        }
        else
        {
            _player = FindObjectOfType<PlayerObject>();
        }
    }

    private void Fire()
    {
        Vector2 playerDir = _player.transform.position - transform.position;
        Bullet bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.isFromEnemy = true;
        bullet.GetComponent<Rigidbody2D>().velocity = (playerDir).normalized * 5f;
    }
}
