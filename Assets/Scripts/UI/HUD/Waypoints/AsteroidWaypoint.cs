using UnityEngine;

public class AsteroidWaypoint : MonoBehaviour
{
    public Asteroid asteroid;
    public Vector2 spriteHalfExtents = new(32, 48); // Temporary constants, add some scaling later
    public void Remove()
    {
        WaypointManager.waypoints.Remove(this);
        Destroy(gameObject);
    }
}
