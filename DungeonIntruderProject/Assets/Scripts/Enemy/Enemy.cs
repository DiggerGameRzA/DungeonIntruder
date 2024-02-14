using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health health;
    [SerializeField] public float speed;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float delayFireTime;
    [SerializeField] private float tempTime;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Player _player;
    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        tempTime = delayFireTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
    }

    void Update()
    {
        if (_player != null)
        {
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
            _player = FindObjectOfType<Player>();
        }
    }

    private void Fire()
    {
        Bullet bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.isFromEnemy = true;
        bullet.GetComponent<Rigidbody2D>().velocity = 
            (transform.position - _player.transform.position).normalized * - 5f;
    }
}
