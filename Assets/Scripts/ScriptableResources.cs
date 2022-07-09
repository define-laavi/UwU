using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Scriptables/Resources")]
public class ScriptableResources : ScriptableObject
{
    public List<Exhibit> inGameExhibits;
}
