using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeContextBinder : MonoBehaviour
{
    public TMPro.TextMeshProUGUI clock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clock.text = DateTime.Now.ToString("hh:mm tt");
    }
}
