using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public Switchable target;

    bool persistentOn;
    GameObject origin;

    public void On(GameObject origin, bool persistent)
    {
        target.On(origin);
        persistentOn = persistent;
        this.origin = origin;
    }

    public void Off(GameObject origin)
    {
        target.Off(origin);
        persistentOn = false;
    }

    void Update()
    {
        if (persistentOn)
        {
            target.On(origin);
        }
    }
}
