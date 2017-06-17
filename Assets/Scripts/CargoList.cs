using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CargoList: List<GameObject>
{
    public int maxCapacity;

    public new bool Add(GameObject cargo)
    {
        if (Count < maxCapacity)
        {
            Add(cargo);
            return true;
        }

        return false;
    }
}
