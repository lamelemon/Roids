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
        camera = GameManager.Instance.Player.GetComponentInChildren<Camera>();
        WaypointImage = GetComponent<Image>();

        // spriteHalfExtents = GetComponent<RectTransform>();
    }
    public void Remove()
    {
        WaypointManager.waypoints.Remove(this);
        Destroy(gameObject);
    }

    public void UpdatePos()
    {
        Vector3 newPos = camera.WorldToScreenPoint(asteroid.transform.position);

        WaypointImage.enabled = newPos.x > camera.pixelWidth 
                                || newPos.y > camera.pixelHeight
                                || newPos.x < 0
                                || newPos.y < 0
                                || newPos.z < 0;

        if (!WaypointImage.enabled) return;

        Vector2 screenCenter = camera.pixelRect.center;

        // Invert x and force y to point down
        Vector2 screenDirection = new(
            newPos.x - screenCenter.x,
            -Mathf.Abs(newPos.y - screenCenter.y)
        );

        if (screenDirection.sqrMagnitude < 1E-3) screenDirection = Vector2.down; // Asteroid is behind screen center
        else screenDirection.Normalize();

        // Asteroid is behind camera
        if (newPos.z < 0) newPos = (screenDirection + screenCenter) * (camera.pixelWidth + camera.pixelHeight);
        
        newPos.x = Mathf.Clamp(newPos.x, spriteHalfExtents.x, camera.pixelWidth - spriteHalfExtents.x);
        newPos.y = Mathf.Clamp(newPos.y, spriteHalfExtents.y, camera.pixelHeight - spriteHalfExtents.y);
        
        transform.SetPositionAndRotation(newPos, Quaternion.Euler(screenDirection));
    }
}
