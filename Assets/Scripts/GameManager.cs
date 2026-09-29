using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Player Player { get; private set; }
    public static GameManager Instance { get; private set; }
    [SerializeField] private Player playerPrefab;
    void Awake()
    {
        Instance = this;
        Player = Instantiate(playerPrefab, transform);
    }
}
