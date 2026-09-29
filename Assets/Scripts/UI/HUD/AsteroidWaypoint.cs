using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidWaypoint : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private GameObject waypointSprite;
    private readonly Dictionary<Asteroid, GameObject> asteroidWaypoints = new();
    void Awake()
    {
        camera = GameManager.Instance.Player.GetComponentInChildren<Camera>();
        AsteroidManager.AsteroidSpawnEvent.AddListener(CreateWaypoint);
    }

    void Update()
    {
        foreach (KeyValuePair<Asteroid, GameObject> pair in asteroidWaypoints)
        {            
            Debug.Log(camera.WorldToScreenPoint(pair.Key.transform.position));
            pair.Value.transform.position = camera.WorldToScreenPoint(pair.Key.transform.position);
            // Instantiate(waypointSprite, camera.WorldToScreenPoint(asteroid.transform.position), Quaternion.identity);
        }
    }

    void CreateWaypoint(Asteroid asteroid)
    {
        asteroidWaypoints.Add(asteroid, waypointSprite);
        AsteroidManager.AsteroidSpawnEvent.RemoveAllListeners();
    }
}
