using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution: MonoBehaviour
{
    List<GameObject> steps = new List<GameObject>();
    public PlayerModel PlayerImpact { get; private set; }
    private WorldModel worldImpact;
    private int index = 0;
    public string Description { get; private set; }


    public Solution(string description, List<GameObject> steps, PlayerModel playerImpact, WorldModel worldImpact)
    {
        this.steps = steps;
        foreach (GameObject step in steps)
        {
            step.GetComponent<Step>().ParentSolution = this;
        }
        this.Description = description;
        this.PlayerImpact = playerImpact;
        this.worldImpact = worldImpact;
    }

    public Solution(string description, List<StepInfo> stepInfos, PlayerModel playerImpact, WorldModel worldImpact)
    {
        foreach (var info in stepInfos)
        {
            GameObject stepTemp = Instantiate(QuestManager.instance.stepPrefabs[info.index], info.position, Quaternion.identity);
            stepTemp.GetComponent<Step>().Description = info.description;
            stepTemp.GetComponent<Step>().ParentSolution = this;
            steps.Add(stepTemp);
        }
        this.Description = description;
        this.PlayerImpact = playerImpact;
        this.worldImpact = worldImpact;
    }

        public void Reset()
    {
        index = 0;
    }

    //progresses the solution
    public void NextStep()
    {
        Debug.Log(index + "/"+steps.Count);
        if (index == steps.Count)
        {
            QuestManager.instance.UpdateModels(PlayerImpact, worldImpact);
            return;
        }
        steps[index++].SetActive(true);
    }
}
