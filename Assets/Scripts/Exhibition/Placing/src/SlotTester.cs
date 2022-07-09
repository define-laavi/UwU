using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTester : MonoBehaviour
{
    public GameObject testCube;
    public Transform objectTransform;

    public SlotType currentType = SlotType.Floor;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectTransform = GameObject.Instantiate(testCube).transform;
        }
        
        if (objectTransform && SlotRaycaster.TryGetTargetedTransformation(Vector2Int.one, currentType, out var transformation))
        {
            objectTransform.position = transformation.position;
            objectTransform.rotation = transformation.rotation;
        }
    }
}
