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

    public float Prestige;

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

        // Calc Prestige

        Dictionary<string, int> tags = new Dictionary<string, int>();

        for (int i = 0; i < objectsInArea.Count; i++)
        {
            if (objectsInArea[i].GetType () != typeof (Exhibit))
            {
                continue;
            }

            Exhibit ex = (Exhibit)objectsInArea[i];

            for (int j = 0; j < ex.Tags.Count; j++)
            {
                string tag = ex.Tags[j].tag;

                if (!tags.ContainsKey(tag))
                {
                    tags.Add(tag, 1);
                } else
                {
                    tags[tag]++;
                }
            }
        }

        Prestige = 0;
        for (int i = 0; i < objectsInArea.Count; i++)
        {
            if (objectsInArea[i].GetType() != typeof(Exhibit))
            {
                continue;
            }

            Exhibit ex = (Exhibit)objectsInArea[i];
            int maxVal = 0;
            for (int j = 0;j < ex.Tags.Count; j++)
            {
                int v = tags[ex.Tags[j].tag];
                if (v > maxVal)
                {
                    maxVal = v;
                }
            }
            Prestige += maxVal - 1;
        }
    }
}
