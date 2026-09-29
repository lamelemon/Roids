using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player { get; private set; }
    public static GameManager Instance { get; private set; }
    [SerializeField] private Player playerPrefab;
    void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab, transform);
    }
}
