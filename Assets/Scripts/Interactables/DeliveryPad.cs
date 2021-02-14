using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cargo>())
        {
            EventManager.TriggerEvent(EventManager.Events.CargoDelivery, other.gameObject);
            Destroy(other.gameObject, 1f); //could be better, maybe call method on cargo to make it inactive, then destroy
        } else
        {
            Winchable winchable = other.gameObject.GetComponent<Winchable>();

            if (winchable != null && winchable.winchPoint.hook != null)
            {
                winchable.winchPoint.hook.Disconnect();
            }
        }
    }
}
