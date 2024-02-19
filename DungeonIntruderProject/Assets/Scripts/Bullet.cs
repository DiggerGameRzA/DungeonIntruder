﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public bool isFromEnemy = false;
    public float damage = 5f;
    public GameObject dmgIndicator;
    public Vector2 indicatorOffset;
    void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger)
        {
            if (isFromEnemy)
            {
                if (col.CompareTag("Player"))
                {
                    float posX = Random.Range(-indicatorOffset.x, indicatorOffset.x);
                    float posY = Random.Range(-indicatorOffset.y, indicatorOffset.y);
                    Vector3 pos = new Vector3(posX, posY, 0);
                    GameObject dmgI = Instantiate(dmgIndicator, transform.position + pos, Quaternion.identity);
                    dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();

                    Destroy(dmgI, 0.5f);
                    col.GetComponent<Player>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (col.CompareTag("Enemy"))
            {
                IGunStats gun = WeaponManager.instance.currentGun;
                damage = gun.Damage + (gun.ModifierInfo.dmgPercentage / 100 * gun.Damage);

                float posX = Random.Range(-indicatorOffset.x, indicatorOffset.x);
                float posY = Random.Range(-indicatorOffset.y, indicatorOffset.y);
                Vector3 pos = new Vector3(posX, posY, 0);
                GameObject dmgI = Instantiate(dmgIndicator, transform.position + pos, Quaternion.identity);
                dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();

                Destroy(dmgI, 0.5f);
                col.GetComponent<Enemy>().health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
