using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public class Asteroid : Damageable
{
    public int spawnWeight = 1;
    [SerializeField] private float movementSpeed;
    public Rigidbody rb { get; private set; }
    private Vector3 target;

    override protected void Awake()
    {
        base.Awake();
        DeathEvent.AddListener(DeathCleanup);
        target = GameManager.Player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed);
    }

    void DeathCleanup()
    {
        AsteroidManager.asteroids.Remove(this);
    }
}
