using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AsteroidWaypoint : MonoBehaviour
{
    public Asteroid asteroid;
    public Vector2 spriteHalfExtents = new(32, 48); // Temporary constants, add some scaling later
    public Image WaypointImage { get; private set; }

    void Awake()
    {
        GameManager.Instance.Player.GetComponentInChildren<Camera>();
        WaypointImage = GetComponent<Image>();
        // spriteHalfExtents = GetComponent<RectTransform>();
    }
    public void Remove()
    {
        WaypointManager.waypoints.Remove(this);
        Destroy(gameObject);
    }

    void Update()
    {
        Vector2 newPos = WaypointManager.Camera.WorldToScreenPoint(asteroid.transform.position);

        WaypointImage.enabled = newPos.x > WaypointManager.Camera.pixelWidth 
                                || newPos.y > WaypointManager.Camera.pixelHeight
                                || newPos.x < 0
                                || newPos.y < 0;

        if (!WaypointImage.enabled) return;
        
        newPos.x = Mathf.Clamp(newPos.x, spriteHalfExtents.x, WaypointManager.Camera.pixelWidth - spriteHalfExtents.x);
        newPos.y = Mathf.Clamp(newPos.y, spriteHalfExtents.y, WaypointManager.Camera.pixelHeight - spriteHalfExtents.y);
        
        transform.SetPositionAndRotation(newPos, Quaternion.FromToRotation(Vector3.zero, newPos));
    }
}
