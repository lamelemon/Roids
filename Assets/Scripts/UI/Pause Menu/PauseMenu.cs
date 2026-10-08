using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    void OnSelect()
    {
        if (menu.enabled) // adding pausing and such later
        {
            menu.enabled = false;
        } else
        {
            menu.enabled = true;
        }
    }
}
