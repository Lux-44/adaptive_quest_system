using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public List<Solution> Solutions  { get; private set; } = new List<Solution>();
    GameObject initialStep;
    public WorldModel WorldPreconditions { get; private set; }

    public Quest(List<Solution> solutions, GameObject initialStep, WorldModel WorldPreconditions)
    {
        this.initialStep = initialStep;
        initialStep.GetComponent<InitialStep>().ParentQuest = this;
        this.Solutions = solutions;
        this.WorldPreconditions = WorldPreconditions;
    }

    public Quest(List<SolutionInfo> solutionInfos, StepInfo initialStep, WorldModel WorldPreconditions)
    {
        GameObject stepTemp = Instantiate(QuestManager.instance.initialStepPrefabs[initialStep.index], initialStep.position, Quaternion.identity);
        stepTemp.GetComponent<InitialStep>().Description = initialStep.description;
        stepTemp.GetComponent<InitialStep>().ParentQuest = this;
        this.initialStep = stepTemp;
        foreach (var sol in solutionInfos)
        {
            Solutions.Add(new Solution(sol.description, sol.stepInfos,  sol.playerModel, sol.worldModel));
        }
        this.WorldPreconditions = WorldPreconditions;
    }

    public void AddSolution(Solution solution)
    {
        Solutions.Add(solution);
    }

    //starts the quests initial step
    public void startQuest()
    {
        initialStep.SetActive(true);
        foreach (Solution sol in Solutions)
        {
            sol.Reset();
        }
    }

    //shows the player possible solutions
    public void ShowSolutions()
    {
        QuestManager.instance.solutionCanvas.SetActive(true);
        QuestManager.instance.dropdown.GetComponent<SelectionHandler>().FillDropdown(Solutions);

    }

    //after the initial step, starts the selected solution
    public void StartSolution(int id)
    {
        Solutions[id].NextStep();
    }
}
