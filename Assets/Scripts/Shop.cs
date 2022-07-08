using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Shop : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        return "Open shop menu";
    }

    public bool Interact()
    {
        OnInteract.Invoke();
        return true;
    }
}
