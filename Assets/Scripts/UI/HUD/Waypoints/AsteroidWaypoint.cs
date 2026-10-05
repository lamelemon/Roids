using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AsteroidWaypoint : MonoBehaviour
{
    public Asteroid asteroid;
    public Vector2 spriteHalfExtents = new(32, 48); // Temporary constants, add some scaling later
    public Image WaypointImage { get; private set; }
    private Camera camera;

    void Awake()
    {
        camera = GameManager.camera;
        WaypointImage = GetComponent<Image>();

        // spriteHalfExtents = GetComponent<RectTransform>();
    }
    public void Remove()
    {
        WaypointManager.waypoints.Remove(this);
        Destroy(gameObject);
    }

    // Update waypoint position and rotation on screen, called by the waypoint manager in its Update()
    public void UpdatePos()
    {
        Vector3 newPos = camera.WorldToScreenPoint(asteroid.transform.position);
        bool behindCamera = newPos.z < 0;

        WaypointImage.enabled = newPos.x > camera.pixelWidth 
                                || newPos.y > camera.pixelHeight
                                || newPos.x < 0
                                || newPos.y < 0
                                || behindCamera;

        if (!WaypointImage.enabled) return;

        Vector2 screenDirection = -(Vector2)newPos - camera.pixelRect.center;

        // Asteroid is behind camera
        if (behindCamera)
        {
            if (screenDirection.sqrMagnitude < 1E-3) screenDirection = Vector2.down;
            else screenDirection.Normalize();

            // Set magnitude to a circle with a radius that goes from the center of the screen to the corners
            // Then adjusts the position to be on the screen
            // If the position goes offscreen it gets clamped later
            newPos = (camera.pixelRect.size.magnitude / 2 * screenDirection) + (camera.pixelRect.size / 2);
        }
        
        // Clamp waypoint within screen bounds
        newPos.x = Mathf.Clamp(newPos.x, spriteHalfExtents.x, camera.pixelWidth - spriteHalfExtents.x);
        newPos.y = Mathf.Clamp(newPos.y, spriteHalfExtents.y, camera.pixelHeight - spriteHalfExtents.y);
        
        transform.SetPositionAndRotation(newPos, Quaternion.identity);
    }
}
