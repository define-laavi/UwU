using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class ShopContextBinder : MonoBehaviour
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
            _context = value;
            iconContext.sprite = value.Icon;
            nameContext.text = value.DisplayName;
            descriptionContext.text = value.Description;
            stateContext.text = ConditionToText(value.Condition);
            valueContext.text = $"${value.Value:0.00}";
            locationContext.text = value.Location;
        }
    }

    private Exhibit _context;

    public int Current
    {
        get => current;
        set
        {
            if (RuntimeConfig.BuyableExhibits.Count == 0)
                return;
            current = Mathf.Clamp(value, 0, RuntimeConfig.BuyableExhibits.Count);
            Context = RuntimeConfig.BuyableExhibits[current];
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
        Current = (Current + 1 + RuntimeConfig.BuyableExhibits.Count) % RuntimeConfig.BuyableExhibits.Count;
    }
    
    public void Previous()
    {
        Current = (Current - 1 + RuntimeConfig.BuyableExhibits.Count) % RuntimeConfig.BuyableExhibits.Count;
    }

    public void Buy()
    {
        if (RuntimeConfig.Money >= _context.Value)
        {
            RuntimeConfig.BuyableExhibits.Remove(_context);
            RuntimeConfig.OwnedExhibits.Add(_context);
            Current = Current;
            RuntimeConfig.Money -= _context.Value;
        }
    }
    
}
