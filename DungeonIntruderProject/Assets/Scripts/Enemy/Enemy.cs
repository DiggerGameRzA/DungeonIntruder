using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health health;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float delayFireTime;
    [SerializeField] private float tempTime;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Player _player;
    void Start()
    {
        tempTime = delayFireTime;
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
