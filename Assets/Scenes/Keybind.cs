using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour
{
    public KeyCode keyCode;
    public bool isMouseButton;
    public int mouseButton;
    public Shadow shadow;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode) || isMouseButton && Input.GetMouseButtonDown(mouseButton))
        {
            shadow.effectDistance = Vector2.zero;
            var transformLocalPosition = transform.localPosition;
            transformLocalPosition.y -= 4;
            transform.localPosition = transformLocalPosition;
        }    
        else if (Input.GetKeyUp(keyCode)|| isMouseButton && Input.GetMouseButtonUp(mouseButton))
        {
            shadow.effectDistance = Vector2.down * 4;
            var transformLocalPosition = transform.localPosition;
            transformLocalPosition.y += 4;
            transform.localPosition = transformLocalPosition;
        }
    }
}
