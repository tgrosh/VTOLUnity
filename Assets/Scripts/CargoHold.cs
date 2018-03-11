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

    public Cargo Peek()
    {
        if (_cargo.Count == 0) return null;

        return this._cargo.First();
    }

    public void Drop(Transform dropPoint)
    {
        if (_cargo.Count == 0) return;

        Cargo cargo = _cargo.First();        
        cargo.gameObject.transform.SetParent(null);
        cargo.gameObject.SetActive(true);
        cargo.transform.position = dropPoint.position - new Vector3(0, cargo.GetBounds().extents.y, 0);
        _cargo.RemoveAt(0);
    }
}
