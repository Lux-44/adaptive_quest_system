using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerModel
{
    public void UpdateModel(PlayerModel other, float weight);

    public Quest SelectQuest(List<Quest> quests);

    public string GetMenuInfo();
}
