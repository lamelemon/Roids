using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    public float TimeAlive { get; private set; }
    public float LifeTime
    {
        get
        {
            return PauseManager.scaledTimeSinceApplicationStart - TimeAlive;
        }
    }
    public float damage;
    public Quaternion moveDirection;

    void Awake()
    {
        TimeAlive = PauseManager.scaledTimeSinceApplicationStart;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.TakeDamage(damage);
        }
    }
}
