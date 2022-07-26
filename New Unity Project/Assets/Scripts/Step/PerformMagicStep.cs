using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformMagicStep : Step
{
    public override string Description { get; set; } = "You apply your magic powers.";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        LogStep();
    }
}
