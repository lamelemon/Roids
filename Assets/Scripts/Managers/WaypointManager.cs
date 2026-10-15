using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : GroupManager
{
    [Tooltip("Min to max scale of waypoints, determined by distance.")]
    public Vector2 waypointScaling;
    [SerializeField] private AsteroidWaypoint waypointPrefab;
    public static new HashSet<AsteroidWaypoint> managed;
    public static Camera Camera { get; private set; }

    override protected void Awake()
    {
        Camera = GameManager.Player.GetComponentInChildren<Camera>();
        AsteroidManager.AsteroidSpawnEvent.AddListener(CreateWaypoint);
        managed = new();
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
        Debug.Log(managed.Count);
    }
}
