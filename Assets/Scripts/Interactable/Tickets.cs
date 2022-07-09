using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Tickets : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        return "Open tickets menu";
    }

    public bool Interact()
    {
        OnInteract.Invoke();
        return true;
    }
}
