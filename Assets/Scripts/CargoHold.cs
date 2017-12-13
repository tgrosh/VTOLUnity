using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CargoHold : MonoBehaviour
{
    private CargoList _cargo = new CargoList();
    public int maxCapacity;

    void Start()
    {
        _cargo.maxCapacity = this.maxCapacity;
    }

    public bool Store(Cargo cargo)
    {
        if (this._cargo.Add(cargo))
        {
            cargo.gameObject.SetActive(false);
            cargo.gameObject.transform.SetParent(this.gameObject.transform);
            return true;
        }

        return false;
    }
}
