using UnityEngine;

// This script is set to run before any other scripts, meaning we can store things here and change them without worrying about other scripts
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool spawnPlayer = true;
    public Player Player;
    public static Camera camera;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Canvas UI;
    void Awake()
    {
        Instance = this;
        if (spawnPlayer && playerPrefab != null)
        {
            Player = Instantiate(playerPrefab, transform);
        }
        camera = Player.GetComponentInChildren<Camera>();
    }
}
