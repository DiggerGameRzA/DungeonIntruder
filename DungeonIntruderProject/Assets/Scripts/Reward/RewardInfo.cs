using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct RewardInfo
{
    [field: SerializeField] public Rarity Rarity { get; private set; }
    
    [field: SerializeField] public bool IsGun { get; private set; }
    [field: SerializeField] public GunStats GunInfo { get; private set; }
    
    [field: SerializeField] public AugmentType AugmentType { get; private set; }
    [field: SerializeField] public float Value { get; private set; }
    // public Item Item { get; private set; }
    
    // public RewardInfo(AugmentType type, float value = 0)
    // {
    //     IsGun = false;
    //     
    //     AugmentType = type;
    //     value = Value;
    // }
    //
    // public RewardInfo(GunStats gunInfo)
    // {
    //     IsGun = true;
    //
    //     GunInfo = gunInfo;
    // }
}

public enum AugmentType
{
    MaxHp,
    MoveSpeed,
    MaxMana,
    Shield,
    
    Gold,
    Item,

    BulletDmg,
    PistolDmg,
    SpellDmg,
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    SuperRare,
    Legendary,
}
