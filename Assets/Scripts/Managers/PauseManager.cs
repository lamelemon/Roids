using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : Manager
{
    public static UnityEvent OnPause { get; private set; } = new();
    public static UnityEvent OnUnPause { get; private set; } = new();
    public static UnityEvent OnTogglePause { get; private set; } = new();

    public static bool IsPaused { get; private set; } = false;

    public MonoBehaviour[] disableOnPause;
    private static readonly List<MonoBehaviour> wasDisabledOnPause = new();
    public static PauseManager instance;

    public static float scaledTimeSinceApplicationStart = 0;

    override protected void Awake()
    {
        instance = this;
    }

    void Update()
    {
        scaledTimeSinceApplicationStart += Time.deltaTime;
        Debug.Log(scaledTimeSinceApplicationStart);
    }

    public static void Pause()
    {
        Debug.Log("Pausing");
        OnPause.Invoke();
        Time.timeScale = 0;
        IsPaused = true;

        foreach (MonoBehaviour script in instance.disableOnPause)
        {
            if (script.enabled)
            {
                script.enabled = false;
                wasDisabledOnPause.Add(script);
            }
        }
    }

    public static void UnPause()
    {
        Debug.Log("Unpausing");
        OnUnPause.Invoke();
        Time.timeScale = 1;
        IsPaused = false;

        foreach (MonoBehaviour script in wasDisabledOnPause)
        {
            if (!script.enabled) script.enabled = true;
        }
        wasDisabledOnPause.Clear();
    }

    public static void TogglePause()
    {
        OnTogglePause.Invoke();
        if (IsPaused) UnPause();
        else Pause();
    }
}
