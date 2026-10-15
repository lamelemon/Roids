using System.Collections.Generic;
using UnityEngine;

// Base manager for anything that handles a group of objects
public class GroupManager : Manager
{
    
    /// <summary>
    /// Hashset of objects managed by this manager
    /// </summary>
    public static HashSet<MonoBehaviour> managed;

    override protected void Awake()
    {
        base.Awake();
        managed = new();
    }

    protected virtual void ToggleManaged(bool enabled)
    {
        if (managed == null || managed.Count == 0) return;
        
        // Fixes an InvalidOperationException where we modify a set while iterating through it
        MonoBehaviour[] iterableManaged = new MonoBehaviour[managed.Count];
        managed.CopyTo(iterableManaged);

        foreach (MonoBehaviour script in iterableManaged)
        {
            if (script != null) script.enabled = enabled;
            else managed.Remove(script);
        }
    }

    protected virtual void OnEnable()
    {
        ToggleManaged(true);
    }

    protected virtual void OnDisable()
    {
        ToggleManaged(false);
    }
}
