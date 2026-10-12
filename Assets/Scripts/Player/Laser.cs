using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    Player player;
    Camera camera;

    void Start()
    {
        player = GameManager.Player;
        camera = player.GetComponentInChildren<Camera>();   
    }

    public void FireLaser()
    {
        if (Physics.Raycast(player.transform.position, camera.transform.forward, out RaycastHit hitInfo) && hitInfo.collider.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.TakeDamage(damage);
        }
    }
}
