using UnityEngine;

public class SlotRaycaster
{
    public static SlotGrid GetActiveGrid()
    {
        var ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f,0));

        if (!Physics.Raycast(ray, out var raycastHit)) return null;

        return raycastHit.transform.TryGetComponent<SlotGrid>(out var slotGrid) ? slotGrid : null;
    }

    public static bool TryGetTargetedTransformation(Vector2Int size, SlotType type, out (Vector3 position, Quaternion rotation) transformation)
    {
        var ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f,0));

        transformation = (Vector3.zero, Quaternion.identity);
        
        if (!Physics.Raycast(ray, out var raycastHit, 4) || !raycastHit.transform.TryGetComponent<SlotGrid>(out var slotGrid))
            return false;

        if (slotGrid.SlotType != type || !slotGrid.Active)
            return false;
        
        transformation = slotGrid.GetClosestGridPoint(raycastHit.point, size);
        return true;
    }
}
