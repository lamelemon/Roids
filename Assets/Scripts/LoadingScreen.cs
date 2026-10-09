using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LoadScene")).allowSceneActivation = true; 
            PlayerPrefs.SetString("LoadScene", "");
        } catch
        {
            Debug.Log("Couldnt load scene! Loading main menu instead.");
            SceneManager.LoadSceneAsync("MainMenu").allowSceneActivation = true;
        }
    }
}
