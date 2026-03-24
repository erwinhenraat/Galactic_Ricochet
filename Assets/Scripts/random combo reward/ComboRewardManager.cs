using System.Collections.Generic;
using UnityEngine;

public class ComboRewardManager : MonoBehaviour
{
    [SerializeField] private List<ComboRewardEntry> rewards;

    private readonly Dictionary<int, Queue<MonoBehaviour>> rewardBags = new();

    private void OnEnable()
    {
        Combo.onComboAchieved += HandleCombo;
        Combo.onComboLost += ResetAllRewards;
        PlayArea.onBallLost += ResetAllRewards;
    }

    private void OnDisable()
    {
        Combo.onComboAchieved -= HandleCombo;
        Combo.onComboLost -= ResetAllRewards;
        PlayArea.onBallLost -= ResetAllRewards;
    }

    private void HandleCombo(int comboLevel, string tag)
    {
        var bag = GetShuffledBag(comboLevel);

        if (bag.Count > 0)
        {
            var reward = bag.Dequeue();

            Debug.Log($"[ComboRewardManager] Reward chosen: {reward.name}");

            // Call method dynamically
            reward.SendMessage("ActivateReward", tag, SendMessageOptions.DontRequireReceiver);
        }
    }

    private Queue<MonoBehaviour> GetShuffledBag(int comboLevel)
    {
        if (!rewardBags.ContainsKey(comboLevel) || rewardBags[comboLevel].Count == 0)
        {
            List<MonoBehaviour> bagList = new();

            foreach (var entry in rewards)
            {
                if (entry.requiredCombo == comboLevel && entry.reward != null)
                {
                    bagList.Add(entry.reward);
                }
            }

            if (bagList.Count == 0)
                return new Queue<MonoBehaviour>();

            // Shuffle
            for (int i = 0; i < bagList.Count; i++)
            {
                int j = Random.Range(i, bagList.Count);
                (bagList[i], bagList[j]) = (bagList[j], bagList[i]);
            }

            rewardBags[comboLevel] = new Queue<MonoBehaviour>(bagList);
        }

        return rewardBags[comboLevel];
    }

    private void ResetAllRewards(int _, string __)
    {
        ResetAllRewards();
    }

    private void ResetAllRewards()
    {
        foreach (var entry in rewards)
        {
            if (entry.reward != null)
            {
                entry.reward.SendMessage("ResetReward", SendMessageOptions.DontRequireReceiver);
            }
        }

        rewardBags.Clear();
    }
}