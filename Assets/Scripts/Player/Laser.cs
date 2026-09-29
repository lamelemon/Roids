using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    Player player;
    Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.Player;
        playerCamera = player.GetComponentInChildren<Camera>();   
    }

    public void FireLaser()
    {
        if (Physics.Raycast(player.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo) && hitInfo.collider.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.TakeDamage(damage);
        }
    }
}
