using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RoomEdit : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent OnInteract;

    public string Highlight()
    {
        return "Edit room theme";
    }

    public bool Interact()
    {
        OnInteract.Invoke();
        return true;
    }
}
