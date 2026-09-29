using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private HashSet<Asteroid> asteroids = new();
    [SerializeField] private float spawnRate = 3f; // how many seconds to wait between spawns
    [SerializeField] private int spawnAmount = 1; // how many asteroids per spawn
    [SerializeField] private Asteroid[] asteroidPrefabs;
    private float lastSpawn = 0;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > lastSpawn + spawnRate) // Spawn asteroid
        {
            lastSpawn = Time.timeSinceLevelLoad;
            foreach (Asteroid asteroid in asteroidPrefabs)
            {
                
            }
        }
    }



}
