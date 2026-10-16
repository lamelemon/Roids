using UnityEngine;

// This script is set to run before any other scripts, meaning we can store things here and change them without worrying about other scripts
public class GameManager : Manager
{
    [SerializeField] private bool spawnPlayer = true;
    public static Player Player;
    [SerializeField] private Player playerPrefab;
    public Canvas UI;
    public SkillTree skillTree;
    public static new GameManager Instance;

    override protected void Awake()
    {
        Instance = this;
        if (spawnPlayer && playerPrefab != null)
        {
            Player = Instantiate(playerPrefab, transform);
        }
    }
}
