using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemStep : Step
{
    public override string Description { get; set; } = "You hand over the item.";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        LogStep();
    }
}
