using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool spawnPlayer = true;
    public Player Player;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Canvas UI;
    void Awake()
    {
        Instance = this;
        if (spawnPlayer && playerPrefab != null)
        {
            Player = Instantiate(playerPrefab, transform);
        }
    }
}
