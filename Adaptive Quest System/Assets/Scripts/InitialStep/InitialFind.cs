using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialFind : InitialStep
{
    public override string Description { get; set; } = "The quest is explained to you";

    public override void StartStep()
    {
        gameObject.SetActive(false);
        ParentQuest.ShowSolutions();
        //LogStep();
    }
}
