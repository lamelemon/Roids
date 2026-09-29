using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Player : Damageable
{
    override protected void Awake()
    {
        base.Awake();
        DeathEvent.AddListener(Death);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("asteroid"))
        {
            TakeDamage(1);
        }
    }

    void Death()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
