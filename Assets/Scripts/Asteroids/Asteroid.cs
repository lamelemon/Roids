using System;
using UnityEngine;

[Serializable]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private int spawnWeight = 1;
    [SerializeField] private int health = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("laser"))
        {
            Destroy(this);
        }
    }
}
