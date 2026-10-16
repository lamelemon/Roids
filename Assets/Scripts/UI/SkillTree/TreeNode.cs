using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TreeNode : MonoBehaviour
{
    public UnityEvent OnActivate { get; private set; } = new();

    public readonly TreeNode[] connections = new TreeNode[6];
    [NonSerialized] public bool active = false;

    public string Buff
    {
        get
        {
            return buffText.text;
        }
        set
        {
            buffText.text = value;
        }
    }

    private TextMeshPro buffText;
    private Image image;

    virtual protected void Awake()
    {
        buffText = GetComponentInChildren<TextMeshPro>();
        image = GetComponent<Image>();

        Color c = image.color;
        c.a = 0.5f;
        image.color = c;
    }

    [ContextMenu("Activate")]
    virtual public void Activate()
    {
        Color c = image.color;
        c.a = 1;
        image.color = c;
        active = true;
        Debug.Log("Activated " + this.GetType());
    }
}
