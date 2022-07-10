using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorbContextBinder : MonoBehaviour
{
    public int currentValue;
    public int levelStartPoint;
    public int nextLevelValue;

    public TMPro.TextMeshProUGUI counter;

    public Material material;

    private float current = 0;
    

    // Update is called once per frame
    void Update()
    {
        current = Mathf.Lerp(current, (currentValue -levelStartPoint) / (float)(nextLevelValue-levelStartPoint), 2f * Time.deltaTime);
        counter.text = $"{currentValue} / {nextLevelValue}";
        material.SetFloat("_Fill",current);
    }
}
