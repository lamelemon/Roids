using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Player : Damageable
{
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
}
