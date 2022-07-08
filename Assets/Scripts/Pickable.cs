using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    string Highlight(Pickable pick);
    GameObject Use(Pickable pick);
}
public class Pickable : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 Rotation;
    public string Name;
}
