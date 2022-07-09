using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IExhibitionObject : MonoBehaviour
{
    [field: SerializeField] public Vector2Int Size { get; private set; }
    [field: SerializeField] public SlotType SlotType { get; private set; }
    [field: SerializeField] public List<ExhibitTag> Tags { get; private set; }

    public abstract void OnPlace();
}

[System.Serializable]
public struct ExhibitTag
{
    public string name;
    public string tag;
    public float strength;
}