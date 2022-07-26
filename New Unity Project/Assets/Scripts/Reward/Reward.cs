using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward 
{
    string description;

    public Reward(string description)
    {
        this.description = description;
    }

    public override string ToString()
    {
        return description;
    }
}
