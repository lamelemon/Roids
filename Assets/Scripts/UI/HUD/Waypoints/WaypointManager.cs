using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private AsteroidWaypoint waypointPrefab;
    public static readonly HashSet<AsteroidWaypoint> waypoints = new();

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
            newPos.x = Mathf.Clamp(newPos.x, waypoint.spriteHalfExtents.x, camera.pixelWidth - waypoint.spriteHalfExtents.x);
            newPos.y = Mathf.Clamp(newPos.y, waypoint.spriteHalfExtents.y, camera.pixelHeight - waypoint.spriteHalfExtents.y);

            waypoint.transform.position = newPos;
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
