using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhibit : IExhibitionObject
{
    [field:Header("Exhibit values")] 
    [field:SerializeField] public Sprite Icon{ get; private set; }
    [field:SerializeField][ field: Multiline]public string Description{ get; private set; }

    [field:SerializeField] [field:Range(0, 100)] public int Condition{ get; private set; }
    [field:SerializeField] public double Value { get; private set; }
    [field:SerializeField] public string Location{ get; private set; }
    
    public override void OnPlace()
    {

    }
}
