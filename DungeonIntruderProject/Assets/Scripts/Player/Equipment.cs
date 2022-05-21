using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public string name;
    public float hp = 0f;
    public float mana = 0f;
    public float shield = 0f;
    public float healingPercentage = 0f;

    public float ammoCapacity = 0f;
    public float collectRange = 0f;

    public float movementPercentage = 0f;
    public float evadeDistance = 0f;
    public float evadeWindow = 0f;
    public float dodgeChance = 0f;

    public float gunDamagePercentage = 0f;
    public float pistolDamagePercentage = 0f;
    public float shotgunDamagePercentage = 0f;
    public float beamDamagePercentage = 0f;
    public float fireRatePercentage = 0f;
    public float freeAmmoChance = 0f;

    public float spellDamagePercentage = 0f;
    public float spellCooldownReductionPercentage = 0f;
    public float spellCostReductionPercentage = 0f;
    public float spellShieldPercentage = 0f;
}
