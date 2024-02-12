using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFromEnemy = false;
    public float damage = 5f;
    public GameObject dmgIndicator;
    public Vector2 indicatorOffset;
    void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D col)
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
            Destroy(gameObject);
        }
    }
}
