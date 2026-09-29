using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private readonly HashSet<Asteroid> asteroids = new();
    [SerializeField] private float spawnRate = 3f; // how many seconds to wait between spawns
    [SerializeField] private int spawnAmount = 1; // how many asteroids per spawn
    [SerializeField] private float maxSpawnDistance = 10f;
    [SerializeField] private float minSpawnDistance = 4f;
    [SerializeField] private Asteroid[] asteroidPrefabs;
    [SerializeField] private float moveSpeed;
    private float sinceLastSpawn = 0;

    private void FixedUpdate()
    {
        sinceLastSpawn += Time.fixedDeltaTime;
        if (sinceLastSpawn >= spawnRate)
        {
            sinceLastSpawn -= spawnRate;
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnPos = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
                Asteroid asteroid = Instantiate(asteroidPrefabs[0], spawnPos, transform.rotation);

                asteroids.Add(asteroid);
            }
        }
    }
}
