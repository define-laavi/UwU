using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RoomBuy : MonoBehaviour, IInteractible
{
    [SerializeField] private int money;

    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        if (RuntimeConfig.Money >= money)
        {
            return $"Buy room for {money}$";
        }
        return $"Not enought money ({money}$)";
    }

    public bool Interact()
    {
        if (RuntimeConfig.Money >= money)
        {
            RuntimeConfig.Money -= money;
            OnInteract.Invoke();
        }
        return false;
    }
}
