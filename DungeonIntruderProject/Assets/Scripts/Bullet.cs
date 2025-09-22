using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public bool isBeam = false;
    public bool isFromEnemy = false;
    public float damage = 5f;
    public GameObject dmgIndicator;
    public Vector2 indicatorOffset;

    private float tempTime = 0f;
    private float delay = 0.2f;
    void Start()
    {
        if (!isBeam)
            Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (tempTime > 0)
            tempTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isBeam)
            return;
        if (col.CompareTag("StageObject"))
        {
            Destroy(gameObject);
        }
        if (isFromEnemy)
        {
            if (col.CompareTag("Player") && col.isTrigger)
            {
                float posX = Random.Range(-indicatorOffset.x, indicatorOffset.x);
                float posY = Random.Range(-indicatorOffset.y, indicatorOffset.y);
                Vector3 pos = new Vector3(posX, posY, 0);
                GameObject dmgI = Instantiate(dmgIndicator, col.transform.position + pos, Quaternion.identity);
                dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();

                float dmgPos = dmgI.transform.position.y;
                dmgI.transform.DOLocalMoveY(dmgPos + 1f, 1f).SetEase(Ease.OutCubic);

                Destroy(dmgI, 1f);
                col.GetComponent<PlayerObject>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (col.CompareTag("Enemy") && col.isTrigger)
        {
            GunStats gun = WeaponManager.Instance.currentGun;
            damage = gun.Damage + (gun.ModifierInfo.dmgPercentage / 100 * gun.Damage);

            float posX = Random.Range(-indicatorOffset.x, indicatorOffset.x);
            float posY = Random.Range(-indicatorOffset.y, indicatorOffset.y);
            Vector3 pos = new Vector3(posX, posY, 0);
            GameObject dmgI = Instantiate(dmgIndicator, col.transform.position + pos, Quaternion.identity);
            dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();
            
            float dmgPos = dmgI.transform.position.y;
            dmgI.transform.DOLocalMoveY(dmgPos + 1f, 1f).SetEase(Ease.OutCubic);

            Destroy(dmgI, 1f);
            col.GetComponent<Enemy>().health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!isBeam)
            return;
        
        if (col.CompareTag("Enemy") && col.isTrigger)
        {
            if (tempTime > 0)
                return;
            
            GunStats gun = WeaponManager.Instance.currentGun;
            damage = gun.Damage + (gun.ModifierInfo.dmgPercentage / 100 * gun.Damage);

            float posX = Random.Range(-indicatorOffset.x, indicatorOffset.x);
            float posY = Random.Range(-indicatorOffset.y, indicatorOffset.y);
            Vector3 pos = new Vector3(posX, posY, 0);
            GameObject dmgI = Instantiate(dmgIndicator, col.transform.position + pos, Quaternion.identity);
            dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();
            
            float dmgPos = dmgI.transform.position.y;
            dmgI.transform.DOLocalMoveY(dmgPos + 1f, 1f).SetEase(Ease.OutCubic);

            Destroy(dmgI, 1f);
            col.GetComponent<Enemy>().health.TakeDamage(damage);
            tempTime = delay;
        }
    }
}
