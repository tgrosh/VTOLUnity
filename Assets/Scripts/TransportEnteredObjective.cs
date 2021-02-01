using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TransportEnteredObjective : Objective
    {
        private void OnTriggerEnter(Collider other)
        {
            EventManager.TriggerEvent("TransportEnteredArea", this.gameObject);
            OnComplete();
        }
    }
}