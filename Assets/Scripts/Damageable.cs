using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected int health = 1;
    public UnityEvent DeathEvent;

    virtual protected void Awake()
    {
        DeathEvent = new();
    }

    virtual public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEvent.Invoke();
            return true;
        }
        return false;
    }
}
