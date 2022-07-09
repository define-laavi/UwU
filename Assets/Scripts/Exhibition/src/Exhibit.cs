using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhibit : IExhibitionObject
{
    [Header("Exhibit values")] 
    [SerializeField] private Sprite icon;
    [SerializeField, Multiline]private string description;

    [SerializeField] [Range(0, 100)] private int condition;
    [SerializeField] private double value;
    [SerializeField] private string location;
    
    public override void OnPlace()
    {

    }
}
