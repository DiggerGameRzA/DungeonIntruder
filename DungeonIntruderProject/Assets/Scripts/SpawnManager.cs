using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Modifier[] mods;
    GameObject[] guns;
    [SerializeField] private Enemy prefabEnemy;
    public Transform[] spawn;

    [SerializeField] private RewardObject rewardObject;

    private int enemyCount = 0;
    void Start()
    {
        // mods = WeaponManager.Instance.mods;
        // guns = WeaponManager.Instance.guns;
        // for (int i = 0; i < spawn.Length; i++)
        // {
        //     GameObject gun = guns[Random.Range(0, guns.Length)];
        //     SpawnAndRandomMod(gun, spawn[i].position);
        // }

        rewardObject.gameObject.SetActive(false);
        foreach (var pos in spawn)
        {
            enemyCount++;
            Instantiate(prefabEnemy, pos.position, Quaternion.identity);
        }
    }

    public void OnEnemyKilled()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            rewardObject.gameObject.SetActive(true);
        }
    }
    void SpawnAndRandomMod(GameObject gun, Vector3 pos)
    {
        if (pos == null) return;
        GameObject go = Instantiate(gun, pos, Quaternion.identity);
        Modifier gunMod = go.GetComponent<IGunStats>().ModifierInfo;
        gunMod.name = mods[Random.Range(0, mods.Length)].name;
        SetModifierInfo(gunMod);
    }
    void SetModifierInfo(Modifier gunMod)
    {
        for (int i = 0; i < mods.Length; i++)
        {
            if (mods[i].name == gunMod.name)
            {
                gunMod.name = mods[i].name;
                gunMod.dmgPercentage = mods[i].dmgPercentage;
                gunMod.ammoCost = mods[i].ammoCost;
                gunMod.fireRatePercentage = mods[i].fireRatePercentage;
                gunMod.bulletVeloPercentage = mods[i].bulletVeloPercentage;
            }
        }
    }
}
