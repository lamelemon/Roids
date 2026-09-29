using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class AsteroidWaypoint : MonoBehaviour
{
    private Camera camera;
    private AsteroidManager asteroidManager;
    [SerializeField] private Sprite waypointSprite;
    void Awake()
    {
        camera = GameManager.Player.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        foreach (Asteroid asteroid in AsteroidManager.asteroids)
        {
            Debug.Log(camera.WorldToScreenPoint(asteroid.transform.position));
            // Instantiate(waypointSprite, camera.WorldToScreenPoint(asteroid.transform.position), Quaternion.identity);
        }
    }
}
