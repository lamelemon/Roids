using System;
using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public class Asteroid : MonoBehaviour
{
    public int spawnWeight = 1;
    [SerializeField] private int health = 1;
    public Rigidbody RigidBody { get; private set; }
    private Player player;

    void Awake()
    {
        player = GameManager.Player;
    }

    void FixedUpdate()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }
}
