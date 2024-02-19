using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunStats : MonoBehaviour, IGunStats
{
    [SerializeField] private string _name = "";
    [SerializeField] private GunType _type = GunType.Pistol;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _cost = 0f;
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private Bullet _bullet = null;
    [SerializeField] private Modifier _modifierInfo;
    public string Name { get { return _name; } }
    public GunType Type { get { return _type; } }
    public float Damage { get { return _damage; } }
    public float FireRate { get { return _fireRate; } }
    public float Cost { get { return _cost; } }
    public float Velocity { get { return _velocity; } }
    public Bullet Bullet { get { return _bullet; } }
    public Modifier ModifierInfo { get { return _modifierInfo; } set { _modifierInfo = value; } }
}
