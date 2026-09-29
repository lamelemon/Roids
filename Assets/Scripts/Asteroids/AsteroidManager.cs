using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static readonly HashSet<Asteroid> asteroids = new();
    [SerializeField] private float spawnRate = 3f; // how many seconds to wait between spawns
    [SerializeField] private int spawnCount = 1; // how many asteroids per spawn
    [SerializeField] private float maxSpawnDistance = 10f;
    [SerializeField] private float minSpawnDistance = 4f;
    [SerializeField] private int maxAsteroids = 10;

    [SerializeField] private Asteroid[] asteroidPrefabs;
    private float sinceLastSpawn = 0;

    private void FixedUpdate()
    {
        sinceLastSpawn += Time.fixedDeltaTime;
        if (sinceLastSpawn >= spawnRate && asteroids.Count < maxAsteroids)
        {
            sinceLastSpawn -= spawnRate;
            int asteroidsToSpawn = Math.Min(maxAsteroids - asteroids.Count, spawnCount);
            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                Vector3 spawnPos = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
                Asteroid asteroid = Instantiate(asteroidPrefabs[0], spawnPos, transform.rotation);
                asteroids.Add(asteroid);
            }
        }
    }
}
