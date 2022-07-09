using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exhibitionist : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private TMPro.TextMeshProUGUI pickableHintText;

    private IExhibitionObject highlightedPickable;
    private SlotGrid highlightedSlotGrid;
    public ExhibitionistState ExhibitionistState { get; private set; }
    void Update()
    {
        pickableHintText.text = "";
        switch (ExhibitionistState)
        {
            case ExhibitionistState.Empty:
                RayTestPick();
                if (highlightedPickable != null && Input.GetMouseButtonDown(1))
                {
                    if (highlightedPickable.TryPick(this, out var objectTransform))
                    {
                        objectTransform.transform.localScale = highlightedPickable.PickedScale;
                        objectTransform.parent = holder;
                        objectTransform.localPosition = Vector3.zero;
                        objectTransform.localRotation = Quaternion.identity;

                        ExhibitionistState = ExhibitionistState.Holding;
                    }
                }
                break;
            case ExhibitionistState.Holding:
                if (SlotRaycaster.TryGetTargetedTransformation(highlightedPickable.Size, highlightedPickable.SlotType,
                        out var transformation))
                    {
                        pickableHintText.text = "Press '<b>LMB</b>' to place";
                        if (Input.GetMouseButtonDown(0))
                        {
                        var pickableTransform = highlightedPickable.transform;
                        pickableTransform.parent = null;
                        pickableTransform.position = transformation.position;
                        pickableTransform.rotation = transformation.rotation;
                        pickableTransform.localScale = Vector3.one;

                        ExhibitionistState = ExhibitionistState.Empty;

                        }
                }
                
                break;
        }
    }

    void RayTestPick()
    {
        highlightedPickable = null;
        pickableHintText.text = "";
        
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (RuntimeConfig.IsBuilding || !Physics.Raycast(ray, out var hit, 2)) return;
        
        var i = hit.collider.GetComponentInParent<IExhibitionObject>();
        if (i == null) return;
        
        highlightedPickable = i;
        pickableHintText.text = $"Press '<b>RMB</b>' to pick up <b>{i.Highlight()}";
    }
}

public enum ExhibitionistState
{
    Empty, Holding
}