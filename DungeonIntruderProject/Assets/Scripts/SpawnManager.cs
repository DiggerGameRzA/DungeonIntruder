using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Modifier[] mods;
    GameObject[] guns;
    public Transform[] spawn;
    void Start()
    {
        mods = WeaponManager.instance.mods;
        guns = WeaponManager.instance.guns;
        for (int i = 0; i < spawn.Length; i++)
        {
            GameObject gun = guns[Random.Range(0, guns.Length)];
            SpawnAndRandomMod(gun, spawn[i].position);
        }
    }
    void SpawnAndRandomMod(GameObject gun, Vector3 pos)
    {
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
