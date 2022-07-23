using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTypeBase : ScriptableObject
{
    public string DisplayName;

    [TextArea]
    public string Description;

    public GameObject TilePrefab;
}
