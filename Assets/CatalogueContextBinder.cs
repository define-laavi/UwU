using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatalogueContextBinder : MonoBehaviour
{
    [SerializeField] private Image iconContext;
    [SerializeField] private TMPro.TextMeshProUGUI nameContext;
    [SerializeField] private TMPro.TextMeshProUGUI descriptionContext;
    [SerializeField] private TMPro.TextMeshProUGUI stateContext;
    [SerializeField] private TMPro.TextMeshProUGUI valueContext;
    [SerializeField] private TMPro.TextMeshProUGUI locationContext;

    public Exhibit Context
    {
        set
        {
            if (value == null)
                return;
            iconContext.sprite = value.Icon;
            nameContext.text = value.DisplayName;
            descriptionContext.text = value.Description;
            stateContext.text = ConditionToText(value.Condition);
            valueContext.text = $"${value.Value:0.00}";
            locationContext.text = value.Location;
        }
    }

    public int Current
    {
        get => current;
        set
        {
            if (RuntimeConfig.OwnedExhibits.Count == 0)
                return;
            current = Mathf.Clamp(value, 0, RuntimeConfig.OwnedExhibits.Count);
            Context = RuntimeConfig.OwnedExhibits[current];
        }
    }

    private int current = 0;

    private void Start()
    {
        Current = 0;
    }

    static string ConditionToText(int condition)
    {
        return condition switch
        {
            < 10 => "Broken Down",
            < 40 => "Falling Apart",
            < 65 => "Few Breaks",
            < 80 => "Visible Scratches",
            < 93 => "Minimal Wear",
            _ => "Perfect"
        };
    }

    public void Next()
    {
        Current = (Current + 1 + RuntimeConfig.OwnedExhibits.Count) % RuntimeConfig.OwnedExhibits.Count;
    }
    
    public void Previous()
    {
        Current = (Current - 1 + RuntimeConfig.OwnedExhibits.Count) % RuntimeConfig.OwnedExhibits.Count;
    }

}
