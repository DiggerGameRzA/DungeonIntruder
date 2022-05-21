using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public string name;
    [Header("Infos")]
    public float dmgPercentage = 0f;
    public float ammoCost = 0f;
    public float fireRatePercentage = 0f;
    public float bulletVeloPercentage = 0;
}
