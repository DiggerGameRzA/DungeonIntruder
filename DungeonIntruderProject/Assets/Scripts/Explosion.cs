using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explosion : MonoBehaviour
{
    public float damage = 1;
    public float radius = 1;
    public GameObject dmgIndicator;

    private float tempTime = 0;
    [SerializeField] private float dmgDelay;

    private void Start()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = radius;
    }

    private void Update()
    {
        if (tempTime > 0)
        {
            tempTime -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (tempTime <= 0)
        {
            if (col.CompareTag("Enemy") && col.isTrigger)
            {
                float posX = Random.Range(-0.1f, 0.1f);
                float posY = Random.Range(-0.1f, 0.1f);
                Vector3 pos = new Vector3(posX, posY, 0);
                GameObject dmgI = Instantiate(dmgIndicator, col.transform.position + pos, Quaternion.identity);
                dmgI.GetComponent<TextMesh>().text = Mathf.RoundToInt(damage).ToString();

                float dmgPos = dmgI.transform.position.y;
                dmgI.transform.DOLocalMoveY(dmgPos + 1f, 1f).SetEase(Ease.OutCubic);

                Destroy(dmgI, 1f);
                col.GetComponent<Enemy>().health.TakeDamage(damage);
                tempTime = dmgDelay;
            }
        }
    }
}
