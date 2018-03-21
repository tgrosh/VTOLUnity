using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour {
    public abstract void On(GameObject origin);
    public abstract void Off(GameObject origin);
}
