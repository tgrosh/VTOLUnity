using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CargoList: List<Cargo>
{
    public int maxCapacity;

    public new bool Add(Cargo cargo)
    {
        if (Count < maxCapacity)
        {
            base.Add(cargo);
            return true;
        }

        return false;
    }
}
