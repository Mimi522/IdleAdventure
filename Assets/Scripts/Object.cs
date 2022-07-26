using UnityEngine;
using System;

/*
-- Scriptable object representing and object that can be placed in a tile.
-- Objects can affect the game upon their placing or when the tile they are
-- placed in is entered by the player.
*/
public class Object : ScriptableObject
{
    public string DisplayName;

    [TextArea] public string Description;

    public GameObject VisualPrefab;

    public event Action OnEventEnded;

    public virtual void OnPlaced() { }
    public virtual void OnEntered() { }

    protected void EndEvent()
    {
        OnEventEnded?.Invoke();
    }
}
