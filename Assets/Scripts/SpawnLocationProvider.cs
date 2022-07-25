using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class SpawnLocationProvider : MonoBehaviour
{
    public List<Vector3> GetLocations()
    {
        var list = new List<Vector3>();

        foreach (Transform child in transform) {
            list.Add(child.position);
        }

        return list;
    }

    private void OnDrawGizmosSelected()
    {
        if (Selection.gameObjects.Contains(transform.gameObject) == false) {
            return;
        }

        foreach (Transform child in transform) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(child.position, 0.2f);
        }
    }
}
