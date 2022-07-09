using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionTester : MonoBehaviour
{
    public List<IExhibitionObject> objects;
    
    private int current;
    public Transform objectTransform;

    private void Start()
    {
        objectTransform = GameObject.Instantiate(objects[current].gameObject).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            current = (current + 1)%objects.Count;
            Destroy(objectTransform.gameObject);
            objectTransform = Instantiate(objects[current].gameObject).transform;
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            current = (current - 1 + objects.Count) % objects.Count;
            Destroy(objectTransform.gameObject);
            objectTransform = Instantiate(objects[current].gameObject).transform;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            objectTransform.GetComponent<IExhibitionObject>().OnPlace();
            objectTransform = Instantiate(objects[current].gameObject).transform;
        }
        print(objects[current].SlotType + " " + objects[current].name);
        if (objectTransform && SlotRaycaster.TryGetTargetedTransformation(objects[current].Size, objects[current].SlotType, out var transformation))
        {
            objectTransform.position = transformation.position;
            objectTransform.rotation = transformation.rotation;
        }
    }
}
