using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDecisionStep : Step
{
    //this goes on the screen
    public override string Description { get; set; } = "You make a moral decision.";

    public string[] optionTexts = new string[2];

    public override void StartStep()
    {
        QuestManager.instance.choiceCanvas.SetActive(true);
        QuestManager.instance.choiceCanvas.GetComponent<ChoiceHandler>().FillDropdown(Description);
    }

    public void MakeChoice(int id)
    {
        QuestManager.instance.choiceCanvas.SetActive(false);

        QuestManager.instance.DisplayText(optionTexts[id]);
        gameObject.SetActive(false);
        ParentSolution.NextStep();
    }
}
