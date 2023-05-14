using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    [SerializeField] private int durbility;

    public Tool(Tool other) : base(other)
    {
        this.durbility = other.durbility;
    }

    public int GetDurbility() { return durbility; }
}
