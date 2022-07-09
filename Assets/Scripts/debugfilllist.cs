using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugfilllist : MonoBehaviour
{
    public List<Exhibit> exhibits;
    
    // Start is called before the first frame update
    void Start()
    {
        RuntimeConfig.OwnedExhibits = exhibits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
