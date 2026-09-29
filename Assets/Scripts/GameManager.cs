using Unity.VectorGraphics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Player Player { get; private set; }
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool spawnPlayer = true;
    [SerializeField] private Player playerPrefab;
    void Awake()
    {
        Instance = this;
        if (spawnPlayer)
        {
            Player = Instantiate(playerPrefab, transform);
        } else
        {
            Player = FindAnyObjectByType<Player>();
        }
    }
}
