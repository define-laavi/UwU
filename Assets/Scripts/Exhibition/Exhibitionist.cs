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

    private int rotation;
    public ExhibitionistState ExhibitionistState { get; private set; }
    void Update()
    {
        pickableHintText.text = "";

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation = (rotation + 1) % 4;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotation = (rotation - 1 + 4) % 4;
        }
        
        switch (ExhibitionistState)
        {
            case ExhibitionistState.Empty:
                RayTestPick();
                if (highlightedPickable != null && Input.GetMouseButtonDown(1))
                {
                    if (highlightedPickable.TryPick(this, out var objectTransform))
                    {
                        ExhibitionArea Area = objectTransform.GetComponentInParent<ExhibitionArea>();

                        RuntimeConfig.IsBuilding = false;
                        objectTransform.parent = null;
                        objectTransform.transform.localScale = highlightedPickable.PickedScale;
                        objectTransform.parent = holder;
                        objectTransform.localPosition = Vector3.zero;
                        objectTransform.localRotation = Quaternion.identity;

                        ExhibitionistState = ExhibitionistState.Holding;
                        highlightedPickable.OnPlace();

                        if (Area)
                        {
                            Area.Recalculate();
                        }
                    }
                }
                break;
            case ExhibitionistState.Holding:
                if (SlotRaycaster.TryGetTargetedTransformation(new Vector2Int((rotation % 2 == 0) ? highlightedPickable.Size.x : highlightedPickable.Size.y,
                            (rotation % 2 == 0) ? highlightedPickable.Size.y : highlightedPickable.Size.x),
                        highlightedPickable.SlotType, out var transformation))
                    {
                        pickableHintText.text = "Press '<b>LMB</b>' to place";
                        if (Input.GetMouseButtonDown(0))
                        {
                        RuntimeConfig.IsBuilding = true;
                        var pickableTransform = highlightedPickable.transform;
                        pickableTransform.parent = null;
                        pickableTransform.position = transformation.position;
                        pickableTransform.rotation = transformation.rotation*Quaternion.Euler(0,rotation*90, 0);
                        pickableTransform.localScale = Vector3.one;
                        pickableTransform.parent = SlotRaycaster.GetActiveGrid().transform;

                        ExhibitionArea Area = pickableTransform.GetComponentInParent<ExhibitionArea>();
                        if (Area != null)
                        {
                            Area.Recalculate();
                        }

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
        if (!Physics.Raycast(ray, out var hit, 2)) return;
        
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