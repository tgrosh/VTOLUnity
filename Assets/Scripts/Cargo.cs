using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour {
    public int volume = 1; //space the object takes up in a cargo hold

    public Bounds GetBounds()
    {
        Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {            
            combinedBounds.Encapsulate(renderer.bounds);
        }

        return combinedBounds;
    }
}
