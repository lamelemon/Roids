using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected int health = 1;
    public UnityEvent DeathEvent { get; private set; }

    virtual protected void Awake()
    {
        DeathEvent = new();
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEvent.Invoke();
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
