using UnityEngine;

public class Object : ScriptableObject
{
    public string DisplayName;

    [TextArea]
    public string Description;

    public GameObject VisualPrefab;
}