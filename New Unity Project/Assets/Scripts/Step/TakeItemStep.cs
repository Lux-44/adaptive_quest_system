using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemStep : Step
{
    public override string Description { get; set; } = "You take the item.";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        LogStep();
    }
}
