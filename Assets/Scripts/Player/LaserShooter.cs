using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using UnityEngine;
using UnityEngine.Events;

public class LaserShooter : MonoBehaviour
{
    public UnityEvent<float> LaserHit { get; private set; } = new();
    public UnityEvent<float> LaserShot { get; private set; } = new();
    [SerializeField] private Laser laserPrefab;
    [SerializeField] private float maxLaserLifeTime = 1f;

    [Tooltip("How many lasers to shoot. Decimals are considered % chances to fire an extra laser.")]
    public float multiShot = 1f;
    [Tooltip("Accuracy of firing multiple lasers in degrees. First laser is always pinpoint accurate.")]
    [Range(0f, 180f)]
    [SerializeField] private float accuracy = 15f;
    [Tooltip("Time to wait before another laser can be fired.")]
    [Min(0)]
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private float laserTravelSpeed = 100f;

    public float BaseDamage
    {
        get
        {
            return m_baseDamage;
        }
        set
        {
            m_baseDamage = value;
            m_damage = value * m_damageMultiplier;
        }
    }
    public float DamageMultiplier
    {
        get
        {
            return m_damageMultiplier;
        }
        set
        {
            m_damageMultiplier = value;
            m_damage = BaseDamage * value;
        }
    }
    [SerializeField] private float m_baseDamage = 1;
    [SerializeField] private float m_damageMultiplier = 1;
    [SerializeField] private float m_damage;

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
        laser.damage = m_damage;
        laser.Rb.linearVelocity = camera.transform.forward * laserTravelSpeed;
    }

    public void FireRay()
    {
        if (
            Physics.Raycast(Vector3.zero, camera.transform.forward, out RaycastHit hitInfo)
            && hitInfo.collider.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid)
            )
        {
            LaserHit.Invoke(m_damage);
            asteroid.TakeDamage(m_damage);
        }
    }

    public void NewFireLasers()
    {
        FireRay();
        if (multiShot > 1)
        {
            int lasersToFire = Mathf.FloorToInt(multiShot);
            lasersToFire += multiShot - lasersToFire < Random.value ? 1 : 0;
            for (int i = 0; i < lasersToFire; i++)
            {
                FireRay();
            }
        }
    }
}
