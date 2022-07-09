using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionArea : MonoBehaviour
{
    public Transform entrance, exit;

    public List<IExhibitionObject> objectsInArea = new List<IExhibitionObject>();
    [Header("Debug")]
    public bool debugRecalculate;
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Color yellowA = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
        Gizmos.color = yellowA;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(entrance?.position ?? Vector3.zero, Vector3.one);
        Gizmos.DrawWireCube(entrance?.position ?? Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(exit?.position ?? Vector3.zero, Vector3.one);
        Gizmos.DrawWireCube(exit?.position ?? Vector3.zero, Vector3.one);
        if (debugRecalculate)
        {
            debugRecalculate = false;
            Recalculate();
        }
    }

    public void Recalculate()
    {
        objectsInArea.Clear();

        objectsInArea.AddRange(transform.GetComponentsInChildren<IExhibitionObject>());
    }
}
