using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemStep : Step
{
    public override string Description { get; set; } = "You collect the item.";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        LogStep();
    }
}
