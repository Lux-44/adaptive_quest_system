using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModelExample : WorldModel
{
    //1=true, -1=false, 0=null 
    public int[] variables = new int[6] { 0, 0, 0, 0,0, 0 };
    List<Reward> rewardedItems = new List<Reward>();
    public List<Reward> RewardedItems { get { return rewardedItems; } set { rewardedItems = value; } }

    public WorldModelExample(List<Reward> rewards)
    {
        rewardedItems = rewards;
    }

    public WorldModelExample()
    {
    }

    public WorldModelExample(int[] vals)
    {
        variables = vals;
    }
    public WorldModelExample(List<Reward> rewards, int[] vals)
    {
        rewardedItems = rewards;
        variables = vals;
    }

    public List<Quest> FilterQuests(List<Quest> quests)
    {
        List<Quest> fitting = new List<Quest>();

        for (int k=0;k<quests.Count;++k)
        {
            bool fits = true;
            for (int i = 0; i < variables.Length; ++i)
            {
                if (!(variables[i] == ((WorldModelExample)quests[k].WorldPreconditions).variables[i] || ((WorldModelExample)quests[k].WorldPreconditions).variables[i] == 0))
                {
                    fits = false;
                }
            }
            if (fits)
            {
                fitting.Add(quests[k]);
                Debug.Log("fits: "+k);
            }
        }

        return fitting;
    }

    public void UpdateModel(WorldModel other)
    {
        for (int i = 0; i < variables.Length; ++i)
        {
            if (((WorldModelExample)other).variables[i] == 1 || ((WorldModelExample)other).variables[i] == -1)
            {
                variables[i] = ((WorldModelExample)other).variables[i];
            }
        }

        foreach (var reward in other.RewardedItems)
        {
            rewardedItems.Add(reward);
            QuestManager.instance.rewardText.text += "\n" + reward;
        }
        PrintModel();
    }

    public void PrintModel()
    {
        QuestManager.instance.worldModelText.text = "World Model:";
        foreach (var variable in variables)
        {
            QuestManager.instance.worldModelText.text += "\n" + variable;
        }
    }
}
