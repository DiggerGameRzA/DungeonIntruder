using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float maxMana = 50f;
    public float currentMana = 50f;
    public float shield = 0f;
    public float healingPercentage = 0f;
    public float collectRange = 50f;

    // [Header("Ammo")]
    // public float currentAmmo = 300f;
    // public float ammoCapacity = 300f;

    [Header("Movement")]
    public float movementSpeed = 50f;
    public float evadeDistance = 0f;
    public float evadeSpeed = 15f;
    public float evadeWindow = 0f;
    public float evadeCooldown = 1f;
    public float dodgeChance = 0f;

    [Header("Gun")]
    public float gunDamagePercentage = 0f;
    public float pistolDamagePercentage = 0f;
    public float shotgunDamagePercentage = 0f;
    public float beamDamagePercentage = 0f;
    public float fireRatePercentage = 0f;
    // public float freeAmmoChance = 0f;

    [Header("Spell")]
    public float spellDamagePercentage = 0f;
    public float spellCooldownReductionPercentage = 0f;
    // public float spellCostReductionPercentage = 0f;
    public float spellShieldPercentage = 0f;
}
