using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelExample : PlayerModel
{
    //(P, T, B, S, M)
    public float[] values = new float[5];

    public PlayerModelExample()
    {
        for (int i = 0; i < values.Length; ++i)
        {
            values[i] = 0f;
        }
    }

    public PlayerModelExample(float[] vals)
    {
        values = vals;
    }

    public string GetMenuInfo()
    {

        return "Power Gamer: " + System.String.Format("{0:0.00}", values[0]) + "\t\t\tTactician: " + System.String.Format("{0:0.00}", values[1]) + "\t\t\tButt-Kicker: " + System.String.Format("{0:0.00}", values[2])
            + "\t\t\tSpecialist: " + System.String.Format("{0:0.00}", values[3]) + "\t\t\tMethod Actor: " + System.String.Format("{0:0.00}", values[4]);
    }

    float InnerProduct(Quest other)
    {
        float[] sumQuest = new float[5];
        foreach (var solution in other.Solutions)
        {
            for (int i = 0; i < sumQuest.Length; ++i)
            {
                sumQuest[i] += ((PlayerModelExample)(solution.PlayerImpact)).values[i];

            }
        }
        Debug.Log("sumQuest: " + sumQuest[0] + " " + sumQuest[1] + " " + sumQuest[2] + " " + sumQuest[3] + " " + sumQuest[4]);
        Debug.Log("player: " + values[0] + " " + values[1] + " " + values[2] + " " + values[3] + " " + values[4]);

        float sum = 0;
        for (int i = 0; i < values.Length; ++i)
        {
            sum += values[i] * sumQuest[i];
        }
        return sum;
    }

    public Quest SelectQuest(List<Quest> quests)
    {
        if (quests.Count == 0)
        {
            return null;
        }

        Quest maxQuest = quests[0];
        float max = InnerProduct(quests[0]);
        int debugHelper = 0;
        for (int i = 1; i < quests.Count; ++i)
        {
            float tmp = InnerProduct(quests[i]);
            Debug.Log(i + " " + tmp);

            if (tmp > max)
            {
                Debug.Log(i + " " + max + " " + tmp);

                max = tmp;
                maxQuest = quests[i];
                debugHelper = i;
            }
        }

        Debug.Log(debugHelper);
        return maxQuest;
    }

    public void UpdateModel(PlayerModel other, float weight)
    {
        for (int i = 0; i < values.Length; ++i)
        {
            values[i] += (((PlayerModelExample)other).values[i] * weight);
        }
    }

}
