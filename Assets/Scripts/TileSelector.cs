using UnityEngine;

/// <summary>
/// Class that does a raycast in order to get the tile that was clicked.
/// </summary>
public class TileSelector : MonoBehaviour
{
    public Tile GetTileClicked(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            return hit.collider.GetComponent<Tile>();
        }

        return null;
    }
}
