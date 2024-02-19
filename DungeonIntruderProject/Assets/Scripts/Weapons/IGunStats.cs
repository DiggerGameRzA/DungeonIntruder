using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunStats
{
    string Name { get; }
    GunType Type { get; }
    float Damage { get; }
    float FireRate { get; }
    float Cost { get; }
    float Velocity { get; }
    Bullet Bullet { get; }
    Modifier ModifierInfo { get; set; }
}
