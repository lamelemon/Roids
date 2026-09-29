using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSession))]
public class DisableARSessionInEditor : MonoBehaviour
{
    #if UNITY_EDITOR
        private void Awake()
        {
            GetComponent<ARSession>().enabled = false;
        }
    #endif
}
