using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
[RequireComponent(typeof(Collider))]
public class Player : Damageable
{
    private Collider collider;
    override protected void Awake()
    {
        GameManager.Player = this;
        base.Awake();
        DeathEvent.AddListener(Death);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            TakeDamage(1);
            asteroid.TakeDamage(1);
        }
    }

    void Death()
    {
        SceneManager.LoadScene("MainMenu");
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
}
