using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionArea : MonoBehaviour
{
    public Transform entrance, exit;
    public bool IsAvailable = false;
    public List<IExhibitionObject> objectsInArea = new List<IExhibitionObject>();
    public Vector2 Area = Vector2.one;

    [Header("Debug")]
    public bool debugRecalculate;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Area.x, 0, Area.y));

        /*
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(entrance?.position ?? Vector3.zero, Vector3.one);
        Gizmos.DrawWireCube(entrance?.position ?? Vector3.zero, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(exit?.position ?? Vector3.zero, Vector3.one);
        Gizmos.DrawWireCube(exit?.position ?? Vector3.zero, Vector3.one);
        */
        if (debugRecalculate)
        {
            debugRecalculate = false;
            Recalculate();
        }
    }

    public void Unlock ()
    {
        IsAvailable = true;
    }

    public void Recalculate()
    {
        objectsInArea.Clear();

        objectsInArea.AddRange(transform.GetComponentsInChildren<IExhibitionObject>());
    }
}
