using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunStats : MonoBehaviour
{
    [SerializeField] private string gunName = "";
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GunType _type = GunType.Pistol;
    [SerializeField] private GunPattern _pattern = GunPattern.Single;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private int _burstRound = 1;
    [SerializeField] private float _burstDelay = 0f;
    [SerializeField] private float _fireRate = 1f;
    // [SerializeField] private float _cost = 0f;
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private Bullet _bullet = null;
    [SerializeField] private Modifier _modifierInfo;
    public string GunName { get { return gunName; } }
    public SpriteRenderer Sprite { get { return _sprite; } }
    public GunType Type { get { return _type; } }
    public GunPattern Pattern { get { return _pattern; } }
    public float Damage { get { return _damage; } }
    public int BurstRound { get { return _burstRound; } }
    public float BurstDelay { get { return _burstDelay; } }
    public float FireRate { get { return _fireRate; } }
    // public float Cost { get { return _cost; } }
    public float Velocity { get { return _velocity; } }
    public Bullet Bullet { get { return _bullet; } }
    public Modifier ModifierInfo { get { return _modifierInfo; } set { _modifierInfo = value; } }
}

public enum GunType
{
    Pistol,
    Shotgun,
    Rifle,
    Special,
}

public enum GunPattern
{
    Single,
    Auto,
    Burst,
    Beam,
}
