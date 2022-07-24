using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : ScriptableObject
{
    public string DisplayName;

    [TextArea]
    public string Description;

    public GameObject TilePrefab;
}
