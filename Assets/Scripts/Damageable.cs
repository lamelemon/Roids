using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int health = 1;

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
            return true;
        }
        return false;
    }
}
