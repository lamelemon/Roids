using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseManager : Manager
{
    public static UnityEvent OnPause { get; private set; } = new();
    public static UnityEvent OnUnPause { get; private set; } = new();
    public static UnityEvent OnTogglePause { get; private set; } = new();

    public static bool IsPaused = false;

    public MonoBehaviour[] disableOnPause;
    private static readonly Queue<MonoBehaviour> wasDisabledOnPause = new();
    public static new PauseManager Instance;

    public static float scaledTimeSinceApplicationStart = 0;

    override protected void Awake()
    {
        base.Awake();
        Instance = this;
    }

    void Update()
    {
        scaledTimeSinceApplicationStart += Time.deltaTime;
    }

    public static void Pause()
    {
        Debug.Log("Pausing");
        OnPause.Invoke();
        Time.timeScale = 0;
        IsPaused = true;

        foreach (MonoBehaviour script in Instance.disableOnPause)
        {
            if (script.enabled)
            {
                wasDisabledOnPause.Enqueue(script);
                script.enabled = false;
            }
        }
    }

    public static void UnPause()
    {
        Debug.Log("Unpausing");
        OnUnPause.Invoke();
        Time.timeScale = 1;
        IsPaused = false;

        while (wasDisabledOnPause.TryPeek(out MonoBehaviour script))
        {
            if (!script.enabled) script.enabled = true;
            wasDisabledOnPause.Dequeue();
        }
    }

    public static bool TogglePause()
    {
        return TogglePause(!IsPaused);
    }

    public static bool TogglePause(bool pause)
    {
        if (pause) Pause();
        else UnPause();
        
        return IsPaused;
    }
}
