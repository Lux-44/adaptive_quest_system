using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WorldModel
{
    public List<Reward> RewardedItems { get; set; }
    public void UpdateModel(WorldModel other);

    public List<Quest> FilterQuests(List<Quest> quests);
}
