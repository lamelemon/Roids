using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : Manager
{
    [Tooltip("Min to max scale of waypoints, determined by distance.")]
    public Vector2 waypointScaling;
    [SerializeField] private AsteroidWaypoint waypointPrefab;
    public static new HashSet<AsteroidWaypoint> managed;
    public static Camera Camera { get; private set; }

    void Awake()
    {
        Camera = GameManager.Player.GetComponentInChildren<Camera>();
        AsteroidManager.AsteroidSpawnEvent.AddListener(CreateWaypoint);
    }

    void CreateWaypoint(Asteroid asteroid)
    {
        AsteroidWaypoint waypoint = Instantiate(waypointPrefab, Camera.WorldToScreenPoint(asteroid.transform.position), Quaternion.identity, parent:transform);
        managed.Add(waypoint);
        waypoint.asteroid = asteroid;
        asteroid.DeathEvent.AddListener(waypoint.Remove);
    }

    void Update()
    {
        foreach (AsteroidWaypoint waypoint in managed)
        {
            waypoint.UpdatePos();
        }
    }
}
