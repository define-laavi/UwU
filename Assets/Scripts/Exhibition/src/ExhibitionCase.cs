using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionCase : IExhibitionObject
{

    public List<SlotGrid> childGrids;

    public override void OnPlace()
    {
        foreach (var childGrid in childGrids)
        {
            childGrid.Active = true;
        }
    }

    public override void OnPickUp()
    {
        foreach (var childGrid in childGrids)
        {
            childGrid.Active = false;
        }
    }
}
