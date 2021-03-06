using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class SlotGrid : MonoBehaviour
{
    [field: SerializeField] public SlotType SlotType { get; private set; }
    [SerializeField]
    private bool _active;
    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            OnActiveChanged();
        }
    }

    [SerializeField] private Vector2 slotSize = Vector2.one * 0.75f;
    [SerializeField] private Vector2 spacingSize = Vector2.one * 0.25f;

    [Header("View"), SerializeField] private GameObject slotViewPrefab;
    
    public (Vector3 position, Quaternion orientation) GetClosestGridPoint(Vector3 hit, Vector2Int selectorSize)
    {
        var gridTransform = transform;
        var matrix = Matrix4x4.TRS(gridTransform.position, gridTransform.rotation, Vector3.one);
        var localHit = matrix.inverse.MultiplyPoint(hit);

        var lossyScale = transform.lossyScale;
        var width = Mathf.FloorToInt(lossyScale.x / (slotSize.x + spacingSize.x));
        var height = Mathf.FloorToInt(lossyScale.z / (slotSize.y+spacingSize.y));

        var objectSize = new Vector3(slotSize.x * selectorSize.x + spacingSize.x * (selectorSize.x - 1), 0.5f,
            slotSize.y * selectorSize.y + spacingSize.y * (selectorSize.y - 1));
        
        //Fix for even spaces
        var fixOffset = new Vector3(((width + 1) % 2) * (slotSize.x + spacingSize.x), 0,
            ((height + 1) % 2) * (slotSize.y + spacingSize.y)) / 2f;

        //Fix for changing selector size
        var selectorOffset = new Vector3((slotSize.x + spacingSize.x) * (selectorSize.x - 1), 0,
            (slotSize.y + spacingSize.y) * (selectorSize.y - 1)) / 2f;

        var output = new Vector3(ToNearestT(localHit.x + fixOffset.x + selectorOffset.x, slotSize.x + spacingSize.x) - fixOffset.x - selectorOffset.x, 0f, ToNearestT(localHit.z + fixOffset.z + selectorOffset.z, slotSize.y + spacingSize.y) - fixOffset.z - selectorOffset.z);
        
        //Fix falling out of bounds
        var distantFixX = 0f;
        if (output.x + objectSize.x / 2f > lossyScale.x / 2f)
            distantFixX = -(slotSize.x + spacingSize.x) * Mathf.Ceil(Mathf.Abs(output.x + objectSize.x / 2f - lossyScale.x / 2f));
        else if (output.x - objectSize.x / 2f < -lossyScale.x / 2f)
            distantFixX = (slotSize.x + spacingSize.x) * Mathf.Ceil(Mathf.Abs(output.x - objectSize.x / 2f + lossyScale.x / 2f));

        var distantFixZ = 0f;
        if (output.z + objectSize.z / 2f > lossyScale.z / 2f)
            distantFixZ = -(slotSize.y + spacingSize.y) * Mathf.Ceil(Mathf.Abs(output.z + objectSize.z / 2f - lossyScale.z / 2f));
        else if (output.z - objectSize.z / 2f < -lossyScale.z / 2f)
            distantFixZ = (slotSize.y + spacingSize.y) * Mathf.Ceil(Mathf.Abs(output.z - objectSize.z / 2f + lossyScale.z / 2f));

        return (matrix.MultiplyPoint(output + new Vector3(distantFixX, 0, distantFixZ)), transform.rotation);
    }

    #region Unity

    private void Start()
    {
        if(Active)
            DrawPreviews();
    }

    private void DrawPreviews()
    {
        var points = GetGridPoints();
        foreach (var point in points)
        {
            var go = GameObject.Instantiate(slotViewPrefab);
            go.transform.localScale = new Vector3(slotSize.x, 0f, slotSize.y);
            go.transform.rotation = transform.rotation;
            go.transform.parent = transform;
            
            go.transform.localPosition = new Vector3(point.x / transform.lossyScale.x, 0, point.z / transform.lossyScale.z);
            _previews.Add(go);
        }
    }   

    private void DestroyPreviews()
    {
        foreach (var preview in _previews)
        {
            Destroy(preview);
        }
        _previews.Clear();
    }

    #endregion

    #region Privates
    private List<Vector3> GetGridPoints()
    {
        var lossyScale = transform.lossyScale;
        var width = Mathf.FloorToInt(lossyScale.x / (slotSize.x + spacingSize.x));
        var height = Mathf.FloorToInt(lossyScale.z / (slotSize.y+spacingSize.y));
        var output = new List<Vector3>();

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                output.Add(new Vector3(i * (slotSize.x + spacingSize.x), 0, j*(slotSize.y + spacingSize.y)) - new Vector3(width* (slotSize.x + spacingSize.x), 0, height* (slotSize.y + spacingSize.y))/2f + new Vector3((slotSize.x + spacingSize.x), 0, (slotSize.y+spacingSize.y))/2f);
            }
        }

        return output;
    }
    private float ToNearestT(float value, float t)
    {
        return Mathf.RoundToInt(value / t) * t;
    }

    private void OnActiveChanged()
    {
        if(Active)
            DrawPreviews();
        else
            DestroyPreviews();
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        var gridTransform = transform;
        var position = gridTransform.position;
        var rotation = gridTransform.rotation;
        
        Gizmos.matrix = Matrix4x4.TRS(position, rotation, gridTransform.lossyScale);
        Gizmos.DrawLine(new Vector3(+0.5f, 0f, +0.5f), new Vector3(+0.5f, 0f, -0.5f));
        Gizmos.DrawLine(new Vector3(+0.5f, 0f, -0.5f), new Vector3(-0.5f, 0f, -0.5f));
        Gizmos.DrawLine(new Vector3(-0.5f, 0f, -0.5f), new Vector3(-0.5f, 0f, +0.5f));
        Gizmos.DrawLine(new Vector3(-0.5f, 0f, +0.5f), new Vector3(+0.5f, 0f, +0.5f));
            
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
        var points = GetGridPoints();
        foreach (var point in points)
        {
            DrawSlot(point);
        }
    }

    private void DrawSlot(Vector3 offset)
    {
        Gizmos.DrawLine(new Vector3(+slotSize.x, 0f, +slotSize.y)/2f + offset, new Vector3(+slotSize.x, 0f, -slotSize.y)/2f + offset);
        Gizmos.DrawLine(new Vector3(+slotSize.x, 0f, -slotSize.y)/2f + offset, new Vector3(-slotSize.x, 0f, -slotSize.y)/2f + offset);
        Gizmos.DrawLine(new Vector3(-slotSize.x, 0f, -slotSize.y)/2f + offset, new Vector3(-slotSize.x, 0f, +slotSize.y)/2f + offset);
        Gizmos.DrawLine(new Vector3(-slotSize.x, 0f, +slotSize.y)/2f + offset, new Vector3(+slotSize.x, 0f, +slotSize.y)/2f + offset);
    }
    #endregion

    private List<GameObject> _previews = new List<GameObject>();

}

public enum SlotType { Floor, Wall, Item }
