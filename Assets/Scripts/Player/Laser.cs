using UnityEngine;

public class Laser : MonoBehaviour
{
    Player player;
    Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = GameManager.Instance.player.GetComponentInChildren<Camera>();   
        player = GameManager.Instance.player;
    }

    public void FireLaser()
    {
        if (Physics.Raycast(player.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo) && hitInfo.collider.gameObject.CompareTag("asteroid"))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }
}
