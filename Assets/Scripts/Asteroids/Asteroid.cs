using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[Serializable]
public class Asteroid : Damageable
{
    public int spawnWeight = 1;
    [SerializeField] private float movementSpeed;
    public Rigidbody Rb { get; private set; }
    private Vector3 target;
    public Collider AsteroidCollider { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        DeathEvent.AddListener(Death);
        Rb = GetComponent<Rigidbody>();
        target = GameManager.Player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed);
    }

    void Death()
    {
        AsteroidManager.managed.Remove(this);
    }
}
