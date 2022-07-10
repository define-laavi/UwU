using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionPicker : IExhibitionObject
{
    [Header("Picker")]
    [SerializeField] private IExhibitionObject exhibitionObject;
    [SerializeField] private int count;
    
    public override string Highlight()
    {
        if (count > 0)
        {
            return $"Press '<b>RMB</b>' to take 1 <b>{DisplayName}";
        }
        return "No more to pick from";
    }

    public override void OnPlace()
    {
    }

    public override bool TryPick(Exhibitionist exhibitionist, out Transform transform)
    {
        if (count > 0)
        {
            count--;
            transform = GameObject.Instantiate(exhibitionObject.gameObject).transform;
            return true;
        }
        transform = null;
        return false;
    }
}
