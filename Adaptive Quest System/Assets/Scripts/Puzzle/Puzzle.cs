using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Puzzle
{
    public string description;

    public Puzzle(string description, Func<bool, bool, bool, bool> func)
    {
        this.description = description;
        eval = func;
    }

    public Func<bool, bool, bool, bool> eval = (a, b, c) => a && b && c;
}
