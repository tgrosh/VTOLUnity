using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switchable : MonoBehaviour {
    public abstract void On(Switch origin);
    public abstract void Off(Switch origin);
}
