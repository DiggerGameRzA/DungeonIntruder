using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _currentHealth = 100f;
    [SerializeField] float _maxHealth = 100f;
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void TakeDamage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDead();
        }
    }

    private void OnDead()
    {
        Destroy(this.gameObject);
    }
}
