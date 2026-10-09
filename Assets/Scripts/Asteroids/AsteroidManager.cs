using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidManager : MonoBehaviour
{
    public static HashSet<Asteroid> asteroids = new();
    [SerializeField]
    [Tooltip("How many seconds to wait between spawns")]
    [Range(1f, 100f)]
    private float spawnRate = 3f;
    [SerializeField] 
    [Tooltip("How many asteroids to spawn per spawn cycle")]
    private int spawnCount = 1;
    [SerializeField]
    [Tooltip("Farthest away an asteorid can spawn to the player")]
    private float maxSpawnDistance = 10f;
    [SerializeField]
    [Tooltip("Closest an asteroid can spawn to the player")]
    private float minSpawnDistance = 4f;
    [Tooltip("How many asteroids can be present at once")]
    [SerializeField] private int maxAsteroids = 10;
    [SerializeField] private Asteroid[] asteroidPrefabs;
    private float sinceLastSpawn = 0;

    public static UnityEvent<Asteroid> AsteroidSpawnEvent = new();

    public int AsteroidsSpawned { get; private set; } = 0;
    public int AsteroidsDestroyed { get; private set; } = 0;
    private int asteroidHealth = 1;

    private void FixedUpdate()
    {
        sinceLastSpawn += Time.fixedDeltaTime;
        if (sinceLastSpawn >= spawnRate && asteroids.Count < maxAsteroids)
        {
            sinceLastSpawn -= spawnRate;
            int asteroidsToSpawn = Math.Min(maxAsteroids - asteroids.Count, spawnCount);
            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                Asteroid asteroid = Instantiate(
                    asteroidPrefabs[0], 
                    UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance),
                    transform.rotation
                );
                asteroids.Add(asteroid);
                AsteroidSpawnEvent.Invoke(asteroid);
                asteroid.DeathEvent.AddListener(OnAsteroidDestroyed);
                AsteroidsSpawned++;
                
                if (AsteroidsSpawned % 10 == 0)
                {
                    asteroidHealth += Mathf.FloorToInt(AsteroidsSpawned * 0.1f);
                } 
            }
        }
    }

    private void OnAsteroidDestroyed()
    {
        AsteroidsDestroyed++;
    }

    private void OnDisable()
    {
        foreach (Asteroid asteroid in asteroids)
        {
            if (asteroid != null) asteroid.enabled = false;
            else asteroids.Remove(asteroid);
        }
    }

    private void OnEnable()
    {
        foreach (Asteroid asteroid in asteroids)
        {
            if (asteroid != null) asteroid.enabled = true;
            else asteroids.Remove(asteroid);
        }
    }
}
