using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Damageable
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("asteroid"))
        {
            Destroy(collision.gameObject);
            if (TakeDamage(1))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
