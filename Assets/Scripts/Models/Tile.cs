using UnityEngine;

/// <summary>
/// Class storing the visual and data of a modifier in the tile.
/// </summary>
public class Tile : MonoBehaviour
{
    public TileModifier ModifierData { get; private set; }

    public TileType TileType { private get; set; } = TileType.Passive;

    private GameObject _modifierVisual;

    public bool TryApplyModifier(TileModifier objectPrefab)
    {
        if (ModifierData != null) {
            return false;
        }

        if (objectPrefab.ValidTarget != TileType) {
            return false;
        }

        Vector3 objectPosition = transform.position;

        ModifierData = objectPrefab;
        _modifierVisual = GameObject.Instantiate(objectPrefab.VisualPrefab, objectPosition, Quaternion.identity);

        ModifierData.OnPlaced();

        return true;
    }
}
