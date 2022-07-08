using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public GameManager Instance;

    [SerializeField] private Image FadeScreen;
    [SerializeField] private float FadeTime;
    [SerializeField] private FirstPersonController PlayerController;

    [SerializeField] private TextMeshProUGUI HintText;

    private Camera _cam;
    private int interactFilter;

    private IInteractible _lastObject;

    void Awake()
    {
        Instance = this;
        FadeScreen.DOFade(0, 0);
    }

    void Start()
    {
        _cam = Camera.main;
        interactFilter = LayerMask.GetMask("Interact");

        HintText.text = "";

        RuntimeConfig.MuzeumOpened = false;
        RuntimeConfig.IsBuilding = false;
        RuntimeConfig.IsMouseLocked = true;

        LockMouse();
    }

    void Update()
    {
        if (RuntimeConfig.IsMouseLocked && !RuntimeConfig.IsBuilding)
        {
            SendRay();
            Interact();
        }
    }

    private void Interact()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        if (_lastObject == null)
        {
            return;
        }
        if (_lastObject.Interact())
        {
            UnlockMouse();
        } else {
            LockMouse();
        }
    }

    private void SendRay()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!RuntimeConfig.IsBuilding && Physics.Raycast(ray, out RaycastHit hit, 2, interactFilter, QueryTriggerInteraction.Ignore))
        {
            IInteractible i = hit.collider.GetComponentInParent<IInteractible>();
            if (i == null)
            {
                print("null");
                _lastObject = null;
                HintText.text = "";

                return;
            }

            if (_lastObject != i)
            {
                HintText.text = i.Highlight();
                _lastObject = i;
            }
        }
        else
        {
            _lastObject = null;
            HintText.text = "";
        }
    }

    public void SwitchMode ()
    {
        if (RuntimeConfig.MuzeumOpened)
        {

        }

        StartCoroutine (Fade(null));
    }

    IEnumerator Fade(UnityAction changeState)
    {
        HintText.text = "";
        PlayerController.enabled = false;
        FadeScreen.DOFade(1, FadeTime * 0.66f);
        yield return new WaitForSeconds(FadeTime * 0.66f);
        changeState?.Invoke();
        FadeScreen.DOFade(0, FadeTime * 0.33f);
        PlayerController.enabled = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RuntimeConfig.IsMouseLocked = true;
        HintText.text = "";
        PlayerController.enabled = true;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        RuntimeConfig.IsMouseLocked = false;
        HintText.text = "";
        PlayerController.enabled = false;
    }
}
