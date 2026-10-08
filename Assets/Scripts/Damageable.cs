using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    protected int maxHealth
    {
        get => m_maxHealth;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException("Max health cannot be under 1");

            if (health == m_maxHealth) health = value;
            m_maxHealth = value;
        }
    }

    [SerializeField] protected int health = 1;
    [SerializeField] private int m_maxHealth = 1;
    [SerializeField] protected bool invincible = false;

    public UnityEvent DeathEvent;

    virtual protected void Awake()
    {
        DeathEvent = new();
        health = maxHealth;
    }

    virtual public bool Heal(int healing)
    {
        if (health + healing <= maxHealth)
        {
            health += healing;
            return true;
        }
        return false;
    }

    virtual public bool TakeDamage(int damage)
    {
        if (invincible) return false; 

        health -= damage;
        if (health <= 0)
        {
            Die();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    [ContextMenu("Take one damage")]
    virtual protected void TakeOneDamage()
    {
        TakeDamage(1);
    }

    [ContextMenu("Die")]
    virtual public void Die()
    {
        DeathEvent.Invoke();
    }
}