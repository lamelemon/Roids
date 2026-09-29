using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Scene loadingScreen;
    [SerializeField] private Scene gameScene;

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
        PlayerPrefs.SetString("LoadScene", "Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
