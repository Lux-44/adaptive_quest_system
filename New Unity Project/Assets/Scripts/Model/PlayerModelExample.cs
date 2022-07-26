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
            values[i] = 20f;
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

    float[] Normalize(float[] vals)
    {
        float[] tmp = new float[5];
        float sum = 0;
        for (int i = 0; i < vals.Length; ++i)
        {
            sum += vals[i];
        }
        float factor = 100 / sum;
        for (int i = 0; i < vals.Length; ++i)
        {
            tmp[i] = vals[i] * factor;
        }
        return tmp;
    }

    float Difference(float[] self, Quest other)
    {
        //sum up all solutions
        float[] sumQuest = new float[5];
        foreach (var solution in other.Solutions)
        {
            for (int i = 0; i < sumQuest.Length; ++i)
            {
                sumQuest[i] += ((PlayerModelExample)(solution.PlayerImpact)).values[i];

            }
        }
        //normalize
        // Debug.Log("self: " + self[0] + " " + self[1] + " " + self[2] + " " + self[3] + " " + self[4]);
        //        Debug.Log("sumQuest: " + sumQuest[0] + " " + sumQuest[1] + " " + sumQuest[2] + " " + sumQuest[3] + " " + sumQuest[4]);
        float[] sumNorm = Normalize(sumQuest);
         Debug.Log("sumNorm: "+ sumNorm[0] +" "+ sumNorm[1] + " " + sumNorm[2] + " " + sumNorm[3] + " " + sumNorm[4]);
        //difference between normalized models
        float sumDiff = 0;
        for (int i = 0; i < sumQuest.Length; ++i)
        {
            sumDiff += Mathf.Abs(self[i] - sumNorm[i]);
            Debug.Log("difference " + i + " " + self[i] +" - "+ sumNorm[i]);
        }
        return sumDiff;
    }

    public Quest SelectQuest(List<Quest> quests)
    {
        if (quests.Count == 0)
        {
            return null;
        }
        float[] normalized = Normalize((float[])values.Clone());
        Quest minQuest = quests[0];
        float minDiff = Difference(normalized, quests[0]);
        Debug.Log("0 " + minDiff);

        int debugHelper = 0;
        for (int i = 1; i < quests.Count; ++i)
        {
            float tmpDiff = Difference(normalized, quests[i]);
            Debug.Log(i + " " + tmpDiff);

            if (tmpDiff < minDiff)
            {
             //   Debug.Log(i + " " + minDiff + " " + tmpDiff);

                minDiff = tmpDiff;
                minQuest = quests[i];
                debugHelper = i;
            }
        }
        Debug.Log(debugHelper);
        return minQuest;
    }

    public void UpdateModel(PlayerModel other, float weight)
    {
        for (int i = 0; i < values.Length; ++i)
        {
            values[i] += (((PlayerModelExample)other).values[i] * weight);
        }
    }

}
