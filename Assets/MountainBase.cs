using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBase : Triggerable
{
    Animator doorAnimator;
    bool doorsOpen;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public override void OnTrigger(GameObject source)
    {
        doorsOpen = !doorsOpen;
        doorAnimator.SetBool("open", doorsOpen);
    }

}
