using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyStep : Step
{
    public override string Description { get; set; } = "You kill the enemy.";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        LogStep();
    }
}
