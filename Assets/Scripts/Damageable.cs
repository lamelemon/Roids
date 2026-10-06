using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected int health = 1;
    [SerializeField] protected bool invincible = false;

    public UnityEvent DeathEvent;

    virtual protected void Awake()
    {
        DeathEvent = new();
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