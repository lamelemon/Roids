using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Tooltip("Min to max scale of waypoints, determined by distance.")]
    public Vector2 waypointScaling;
    [SerializeField] private AsteroidWaypoint waypointPrefab;
    public static readonly HashSet<AsteroidWaypoint> waypoints = new();
    public static Camera Camera { get; private set; }

    void Awake()
    {
        Camera = GameManager.Instance.Player.GetComponentInChildren<Camera>();
        AsteroidManager.AsteroidSpawnEvent.AddListener(CreateWaypoint);
    }

    void CreateWaypoint(Asteroid asteroid)
    {
        AsteroidWaypoint waypoint = Instantiate(waypointPrefab, Camera.WorldToScreenPoint(asteroid.transform.position), Quaternion.identity, parent:transform);
        waypoints.Add(waypoint);
        waypoint.asteroid = asteroid;
        asteroid.DeathEvent.AddListener(waypoint.Remove);
    }
}
