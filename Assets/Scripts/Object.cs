using UnityEngine;
using System;

public class Object : ScriptableObject
{
    public string DisplayName;

    [TextArea]
    public string Description;

    public GameObject VisualPrefab;

    public event Action OnEventEnded;

    public virtual void OnPlaced() { }
    public virtual void OnEntered() { }

    protected void EndEvent()
    {
        OnEventEnded?.Invoke();
    }
}
