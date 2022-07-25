using UnityEngine;
using System;

public class Object : ScriptableObject
{
    public string DisplayName;

    [TextArea]
    public string Description;

    public GameObject VisualPrefab;

    public event Action OnEventEnded;

    public virtual void OnPlaced(GameObject player) { }
    public virtual void OnEntered(GameObject player) { }

    protected void EndEvent()
    {
        OnEventEnded?.Invoke();
    }
}
