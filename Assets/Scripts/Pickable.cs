using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    string Highlight();

    /// <returns>if lock movement</returns>
    bool Interact();
}
public class Pickable : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 Rotation;
    public string Name;
}
