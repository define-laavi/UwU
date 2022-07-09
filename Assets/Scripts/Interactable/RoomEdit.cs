using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RoomEdit : MonoBehaviour, IInteractible
{
    [SerializeField] private TextMeshPro ExibitionTitle;
    [SerializeField] private UnityEvent OnInteract;

    private bool _enable = false;

    void Start ()
    {
        ExibitionTitle.text = "";
    }

    public string Highlight()
    {
        if (_enable)
        {
            return "";
        }

        return "Edit room theme";
    }

    public bool Interact()
    {
        if (_enable)
        {
            return false;
        }

        OnInteract.Invoke();
        return true;
    }

    public void EnableEdit ()
    {
        _enable = true;
    }
}
