using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    [SerializeField] [SyncVar] float _currentHealth = 100f;
    [SerializeField] float _maxHealth = 100f;
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    [SerializeField] private Slider healthBar;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void TakeDamage(float dmg)
    {
        _currentHealth -= dmg;
        if (healthBar != null)
        {
            healthBar.value = _currentHealth;
        }
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDead();
        }
    }

    private void OnDead()
    {
        // FindObjectOfType<SpawnManager>()?.OnEnemyKilled();
        Destroy(this.gameObject);
    }
}
