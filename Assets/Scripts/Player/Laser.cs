using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Laser : MonoBehaviour
{
    private float TimeAtCreation;
    public float LifeTime
    {
        get
        {
            return PauseManager.scaledTimeSinceApplicationStart - TimeAtCreation;
        }
    }
    public float damage;
    public Rigidbody Rb { get; private set; }

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        TimeAtCreation = PauseManager.scaledTimeSinceApplicationStart;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.TakeDamage(damage);
            this.enabled = false;
        }
        Debug.Log("Collision with " + collision.gameObject.name);
    }
}
