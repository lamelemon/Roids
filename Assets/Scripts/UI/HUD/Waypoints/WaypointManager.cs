using System;
using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaypointManager : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private AsteroidWaypoint waypointPrefab;
    private readonly HashSet<AsteroidWaypoint> waypoints = new();

    void Awake()
    {
        camera = GameManager.Instance.Player.GetComponentInChildren<Camera>();
        AsteroidManager.AsteroidSpawnEvent.AddListener(CreateWaypoint);
    }

    void Update()
    {
        foreach (AsteroidWaypoint waypoint in waypoints)
        {            
            Vector2 newPos = camera.WorldToScreenPoint(waypoint.asteroid.transform.position);
            newPos.x = Mathf.Clamp(newPos.x, 0, camera.pixelWidth);
            newPos.y = Mathf.Clamp(newPos.y, 0, camera.pixelHeight);

            waypoint.transform.position = camera.WorldToScreenPoint(newPos);
        }
    }

    void CreateWaypoint(Asteroid asteroid)
    {
        AsteroidWaypoint waypoint = Instantiate(waypointPrefab, camera.WorldToScreenPoint(asteroid.transform.position), Quaternion.identity, parent:transform);
        waypoints.Add(waypoint);
        waypoint.asteroid = asteroid;
        asteroid.DeathEvent.AddListener(waypoint.Remove);
    }
}
