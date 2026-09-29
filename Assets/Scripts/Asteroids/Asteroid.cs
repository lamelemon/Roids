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

    void Awake()
    {
        target = GameManager.Player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed);
    }
}
