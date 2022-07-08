using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRaycaster : MonoBehaviour
{
    void Update()
    {
        var ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f,0));

        if (!Physics.Raycast(ray, out var raycastHit)) return;
        
        if (raycastHit.transform.TryGetComponent<SlotGrid>(out var slotGrid))
        {
            slotGrid.hitPoint = raycastHit.point;
        }
    }
}
