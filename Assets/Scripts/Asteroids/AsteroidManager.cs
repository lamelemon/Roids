using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private HashSet<Asteroid> asteroids = new();
    [SerializeField] private float spawnRate = 3f; // how many seconds to wait between spawns
    [SerializeField] private int spawnAmount = 1; // how many asteroids per spawn
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private Asteroid[] asteroidPrefabs;
    private float lastSpawn = 0;
    private Player player;


    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > lastSpawn + spawnRate) // Spawn asteroid
        {
            lastSpawn = Time.timeSinceLevelLoad;
            Vector3 spawnPos = UnityEngine.Random.insideUnitSphere * spawnDistance;

            // foreach (Asteroid asteroid in asteroidPrefabs)
            // {
                
            // }
        }
    }



}
