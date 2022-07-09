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
    public static GameManager Instance;

    [SerializeField] private Image FadeScreen;
    [SerializeField] private float FadeTime;
    [SerializeField] private FirstPersonController PlayerController;
    [SerializeField] private TextMeshProUGUI HintText;

    [SerializeField] private Transform DayLogScreen;

    [SerializeField] private Transform OpenMuzeumPlayerPosition;
    [SerializeField] private Transform CloseMuzuemPlayerPosition;

    [SerializeField] private Transform[] Menus;

    private OpenMuzeumManager _openerManager;

    private Camera _cam;
    private int interactFilter;

    private IInteractible _lastObject;

    void Awake()
    {
        Instance = this;
        FadeScreen.DOFade(0, 0);
        _openerManager = GetComponent<OpenMuzeumManager>();
    }

    void Start()
    {
        _cam = Camera.main;
        interactFilter = LayerMask.GetMask("Interact");

        HintText.text = "";

        RuntimeConfig.MuzeumOpened = false;
        RuntimeConfig.IsBuilding = false;

        LockMouse();
    }

    [SerializeField] private float _timer = 0;

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
        if (!RuntimeConfig.MuzeumOpened)
        {
            StartCoroutine (FadeOpenMuzeum());
        } else
        {
            StartCoroutine(FadeCloseMuzeum());
        }

    }

    IEnumerator FadeCloseMuzeum ()
    {
        HintText.text = "";
        PlayerController.enabled = false;

        _openerManager.enabled = false;

        FadeScreen.DOFade(1, FadeTime * 0.66f);
        yield return new WaitForSeconds(FadeTime * 0.66f);
        PlayerController.transform.SetPositionAndRotation(CloseMuzuemPlayerPosition.position, CloseMuzuemPlayerPosition.rotation);
        RuntimeConfig.MuzeumOpened = false;
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].gameObject.SetActive(false);
        }
        DayLogScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(FadeTime * 0.5f);
        UnlockMouse();
        FadeScreen.DOFade(0, FadeTime * 0.33f);
        yield return new WaitForSeconds(FadeTime * 0.33f);
    }

    IEnumerator FadeOpenMuzeum()
    {
        HintText.text = "";
        PlayerController.enabled = false;
        FadeScreen.DOFade(1, FadeTime * 0.66f);
        yield return new WaitForSeconds(FadeTime * 0.66f);
        PlayerController.transform.SetPositionAndRotation(OpenMuzeumPlayerPosition.position, OpenMuzeumPlayerPosition.rotation);
        RuntimeConfig.MuzeumOpened = true;
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].gameObject.SetActive(false);
        }
        _openerManager.enabled = true;
        yield return new WaitForSeconds(FadeTime * 0.5f);
        PlayerController.enabled = true;
        LockMouse();
        FadeScreen.DOFade(0, FadeTime * 0.33f);
        yield return new WaitForSeconds(FadeTime * 0.33f);

        _openerManager.enabled = true;
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
