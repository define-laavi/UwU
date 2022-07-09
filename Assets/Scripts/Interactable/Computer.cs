using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Computer : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        return "Sit on PC";
    }

    public bool Interact()
    {
        OnInteract.Invoke();
        return true;
    }
}
