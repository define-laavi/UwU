using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IExhibitionObject : MonoBehaviour
{
    [field: Header("Pickable")]
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Vector3 PickedScale { get; set; }
    [field: Header("Placeable")]
    [field: SerializeField] public Vector2Int Size { get; private set; }
    [field: SerializeField] public SlotType SlotType { get; private set; }
    [field: SerializeField] public List<ExhibitTag> Tags { get; private set; }

    public abstract void OnPlace();
    public virtual void OnPickUp(){}
    public virtual string Highlight()
    {
        return $"Press '<b>RMB</b>' to pick up <b>{DisplayName}";
    }

    public virtual bool TryPick(Exhibitionist exhibitionist, out Transform transform)
    {
        if (exhibitionist.ExhibitionistState == ExhibitionistState.Holding)
        {
            transform = null;
            return false;
        }

        OnPickUp();
        transform = this.transform;
        return true;
    }
}

[System.Serializable]
public struct ExhibitTag
{
    public string name;
    public string tag;
    public float strength;
}