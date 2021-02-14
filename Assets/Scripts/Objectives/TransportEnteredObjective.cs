using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TransportEnteredObjective : Objective
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Transport>())
            {
                OnComplete();
            }
        }
    }
}