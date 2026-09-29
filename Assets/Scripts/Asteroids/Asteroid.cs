using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public class Asteroid : Damageable
{
    public int spawnWeight = 1;
    [SerializeField] private float movementSpeed;
    public Rigidbody Rb { get; private set; }
    private Vector3 target;
    public Image waypoint;

    override protected void Awake()
    {
        base.Awake();
        DeathEvent.AddListener(DeathCleanup);
        Rb = GetComponent<Rigidbody>();
        target = GameManager.Instance.Player.transform.position;
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
