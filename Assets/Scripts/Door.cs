using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Door : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        return RuntimeConfig.MuzeumOpened ? "Close Muzeum" : "Open Muzeum";
    }

    public bool Interact()
    {
        OnInteract.Invoke();
        return true;
    }
}
