using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    protected int MaxHealth
    {
        get => m_maxHealth;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException("Max health cannot be under 1");

            if (health == m_maxHealth && startMaxHealth) health = value;
            m_maxHealth = value;
        }
    }

    [SerializeField] protected float health = 1;
    [SerializeField] private int m_maxHealth = 1;
    [SerializeField] protected bool invincible = false;
    [SerializeField] protected bool startMaxHealth = true;
    protected bool wasHealed = false;

    public UnityEvent DeathEvent;

    virtual protected void Awake()
    {
        DeathEvent = new();
        if (startMaxHealth) health = MaxHealth;
    }

    virtual public bool Heal(int healing)
    {
        if (health + healing <= MaxHealth)
        {
            health += healing;
            wasHealed = true;
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
        Destroy(gameObject);
    }
}