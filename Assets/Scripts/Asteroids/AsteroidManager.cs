using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private HashSet<GameObject> asteroids = new();
    [SerializeField] private float spawnRate = 3f; // how many seconds to wait between spawns
    [SerializeField] private int spawnAmount = 1; // how many asteroids per spawn
    [SerializeField] private float maxSpawnDistance = 10f;
    [SerializeField] private float minSpawnDistance = 4f;
    [SerializeField] private GameObject[] asteroidPrefabs;
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
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnPos = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
                asteroids.Add(Instantiate(asteroidPrefabs[0], spawnPos, transform.rotation)); 
            }
        }
    }
}
