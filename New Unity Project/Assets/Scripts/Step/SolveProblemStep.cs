using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveProblemStep : Step
{
    public override string Description { get; set; } = "You solve the puzzle.";

    public override void StartStep()
    {
        QuestManager.instance.puzzleCanvas.SetActive(true);
        QuestManager.instance.puzzleCanvas.GetComponent<LogicalPuzzleHandler>().FillPuzzle();
    }

    public void Correct()
    {
        gameObject.SetActive(false);
        ParentSolution.NextStep();
        QuestManager.instance.DisplayText("You correctly solve the puzzle.");
    }

    public void Incorrect()
    {

        QuestManager.instance.DisplayText("You didn't solve the puzzle, try again.");

    }
}
