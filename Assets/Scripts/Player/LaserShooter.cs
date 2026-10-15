using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private Laser laserPrefab;
    [SerializeField] private float maxLaserLifeTime = 5.0f;

    [SerializeField] private int damage = 1;
    [Tooltip("How many lasers to shoot. Decimals are considered % chances to fire an extra laser.")]
    [SerializeField] private float multiShot = 1f;
    [Tooltip("Accuracy of firing multiple lasers in degrees. First laser is always pinpoint accurate.")]
    [Range(0f, 180f)]
    [SerializeField] private float accuracy = 15f;
    [Tooltip("Time to wait before another laser can be fired.")]
    [Min(0)]
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private float laserTravelSpeed = 100f;

    private Queue<Laser> lasers;

    Player player;
    Camera camera;

    void Awake()
    {
        lasers = new();
    }

    void Start()
    {
        player = GameManager.Player;
        camera = player.GetComponentInChildren<Camera>();   
    }

    void Update()
    {
        while (lasers.TryPeek(out Laser laser) && laser.LifeTime >= maxLaserLifeTime)
        {
            Destroy(laser.gameObject);
            lasers.Dequeue();
        }
    }

    public void FireLaser()
    {
        Laser laser = Instantiate(laserPrefab, player.transform.position, camera.transform.rotation);
        lasers.Enqueue(laser);
        laser.damage = damage;
    }

    public void NewFireLasers()
    {
        FireLaser();
        if (multiShot > 1)
        {
            int lasersToFire = Mathf.FloorToInt(multiShot);
            lasersToFire += multiShot - lasersToFire < Random.value ? 1 : 0;
            for (int i = 0; i < lasersToFire; i++)
            {
                FireLaser();
            }
        }
    }
}
