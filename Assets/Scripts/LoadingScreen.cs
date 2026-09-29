using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LoadScene")).allowSceneActivation = true;
    }
}
