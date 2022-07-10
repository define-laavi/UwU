using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Exhibitionist : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private TMPro.TextMeshProUGUI pickableHintText;

    private IExhibitionObject highlightedPickable;
    private SlotGrid highlightedSlotGrid;

    private int rotation;
    public ExhibitionistState ExhibitionistState { get; private set; }

    [Header("Description")]
    public float MoveThreshold = 0.1f;
    public float ShowedMoveThreshold = 0.3f;
    [Range(0.2f, 5f)] public float ShowDelay;
    [SerializeField] private Transform DescriptionMonit;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private RectTransform DescriptionScaler;
    private bool _descriptionShow = false;
    private float _timer = 0;
    private Vector3 _startCountingPosition;

    void Start()
    {
        _startCountingPosition = transform.position;
    }

    void Update()
    {
        Description();

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
                        highlightedPickable = objectTransform.GetComponent<IExhibitionObject>();
                        RuntimeConfig.IsBuilding = false;
                        objectTransform.parent = null;
                        objectTransform.transform.localScale = highlightedPickable.PickedScale;
                        objectTransform.parent = holder;
                        objectTransform.localPosition = Vector3.zero;
                        objectTransform.localRotation = Quaternion.identity;

                        ExhibitionistState = ExhibitionistState.Holding;
                        highlightedPickable.OnPlace();
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
                        pickableTransform.rotation = transformation.rotation * Quaternion.Euler(0, rotation * 90, 0);
                        pickableTransform.localScale = Vector3.one;
                        pickableTransform.parent = SlotRaycaster.GetActiveGrid().transform;


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
        pickableHintText.text = i.Highlight();
    }

    private void Description()
    {
        if (ExhibitionistState != ExhibitionistState.Holding)
        {
            return;
        }

        if (highlightedPickable == null || highlightedPickable.GetType() != typeof(Exhibit))
        {
            return;
        }

        float smag = (transform.position - _startCountingPosition).sqrMagnitude;
        if (_descriptionShow)
        {
            if (smag > ShowedMoveThreshold)
            {
                _descriptionShow = false;
                group.DOKill();
                group.DOFade(0, 2 * group.alpha / 1f);
                DescriptionScaler.DOKill();
                DescriptionScaler.DOSizeDelta(Vector2.zero, 2f * group.alpha /1f);
            }
        } else {
            if (smag > MoveThreshold)
            {
                _startCountingPosition = transform.position;
                _timer = 0;
                return;
            }
        }

        _timer += Time.deltaTime;

        if (_timer < ShowDelay)
        {
            return;
        }

        _timer = float.NegativeInfinity;
        Exhibit exh = (Exhibit)highlightedPickable;
        DescriptionText.text = "<align=\"center\">" + exh.DisplayName + "</align>\n" + exh.Description;
        _descriptionShow = true;

        StartCoroutine(Open());
    }

    IEnumerator Open ()
    {
        
        group.DOFade(1, 2f);
        DescriptionScaler.DOSizeDelta(new Vector2(200, 250), 2f);

        yield return new WaitForSeconds(2f);
    }
}

public enum ExhibitionistState
{
    Empty, Holding
}