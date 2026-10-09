using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Canvas menu;
    void Awake()
    {
        menu = GetComponentInChildren<Canvas>(true);
        menu.enabled = false;

        PauseManager.OnPause.AddListener(OnPause);
        PauseManager.OnUnPause.AddListener(OnUnPause);
    }

    void OnPause()
    {
        menu.enabled = true;
    }

    void OnUnPause()
    {
        menu.enabled = false;
    }

    public void Toggle()
    {
        menu.enabled = !menu.enabled;
        PauseManager.TogglePause();
    }

    public void Resume()
    {
        PauseManager.UnPause();
    }

    public void SkillTree()
    {
        Debug.Log("Skill tree wow");
    }

    public void Options()
    {
        Debug.Log("Options wow");
    }

    public void Quit()
    {
        SceneManager.LoadScene("LoadingScreen");
        PlayerPrefs.SetString("LoadScene", "MainMenu");
    }
}
