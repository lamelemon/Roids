using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AsteroidWaypoint : MonoBehaviour
{
    public Asteroid asteroid;
    public Vector2 spriteHalfExtents;
    public Image WaypointImage { get; private set; }
    private Camera camera;

    void Awake()
    {
        camera = GameManager.camera;
        WaypointImage = GetComponent<Image>();
        spriteHalfExtents = WaypointImage.rectTransform.GetScreenRect().size / 2;

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
        WaypointImage.enabled = behindCamera
                                || newPos.x > camera.pixelWidth 
                                || newPos.y > camera.pixelHeight
                                || newPos.x < 0
                                || newPos.y < 0;
                                
        if (!WaypointImage.enabled) return;

        Vector2 screenDirection = -(Vector2)newPos - camera.pixelRect.center;

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
        newPos.Set(
            Mathf.Clamp(newPos.x, spriteHalfExtents.x, camera.pixelWidth - spriteHalfExtents.x),
            Mathf.Clamp(newPos.y, spriteHalfExtents.y, camera.pixelHeight - spriteHalfExtents.y),
            0
        );

        transform.position = newPos;
    }
}
