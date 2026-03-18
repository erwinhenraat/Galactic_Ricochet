using System.Collections.Generic;
using UnityEngine;

public class ComboRewardManager : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> rewardBehaviours;

    private readonly List<IComboReward> rewards = new();

    // Shuffle bags per combo level
    private readonly Dictionary<int, Queue<IComboReward>> rewardBags = new();

    private void Awake()
    {
        foreach (var behaviour in rewardBehaviours)
        {
            if (behaviour is IComboReward reward)
            {
                rewards.Add(reward);
                Debug.Log($"[ComboRewardManager] Registered reward: {behaviour.name}");
            }
            else
            {
                Debug.LogError($"{behaviour.name} does not implement IComboReward");
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log("[ComboRewardManager] Enabled");
        Combo.onComboAchieved += HandleCombo;
        Combo.onComboLost += ResetAllRewards;
        PlayArea.onBallLost += ResetAllRewards;
    }

    private void OnDisable()
    {
        Debug.Log("[ComboRewardManager] Disabled");
        Combo.onComboAchieved -= HandleCombo;
        Combo.onComboLost -= ResetAllRewards;
        PlayArea.onBallLost -= ResetAllRewards;
    }

    private void HandleCombo(int comboLevel, string tag)
    {
        Debug.Log($"[ComboRewardManager] Combo achieved: {comboLevel}, Tag: {tag}");

        var bag = GetShuffledBag(comboLevel);

        if (bag.Count > 0)
        {
            IComboReward chosenReward = bag.Dequeue();

            Debug.Log($"[ComboRewardManager] Bag reward chosen: {chosenReward}");
            chosenReward.ActivateReward(tag);
        }
        else
        {
            Debug.LogWarning($"[ComboRewardManager] No rewards available for combo level {comboLevel}");
        }
    }

    private Queue<IComboReward> GetShuffledBag(int comboLevel)
    {
        // If no bag exists OR it's empty → rebuild and reshuffle
        if (!rewardBags.ContainsKey(comboLevel) || rewardBags[comboLevel].Count == 0)
        {
            List<IComboReward> bagList = new();

            foreach (var reward in rewards)
            {
                if (reward.RequiredCombo == comboLevel)
                {
                    bagList.Add(reward);
                }
            }

            if (bagList.Count == 0)
            {
                Debug.LogWarning($"[ComboRewardManager] No rewards found for combo level {comboLevel}");
                return new Queue<IComboReward>();
            }

            // Fisher-Yates shuffle
            for (int i = 0; i < bagList.Count; i++)
            {
                int j = Random.Range(i, bagList.Count);
                (bagList[i], bagList[j]) = (bagList[j], bagList[i]);
            }

            rewardBags[comboLevel] = new Queue<IComboReward>(bagList);

            Debug.Log($"[ComboRewardManager] Rebuilt shuffle bag for combo {comboLevel} with {bagList.Count} rewards");
        }

        return rewardBags[comboLevel];
    }

    private void ResetAllRewards(int _, string __)
    {
        Debug.Log("[ComboRewardManager] Resetting all rewards");
        ResetAllRewards();
    }

    private void ResetAllRewards()
    {
        foreach (var reward in rewards)
        {
            reward.ResetReward();
        }

        // Optional: clear bags on reset so randomness fully refreshes
        rewardBags.Clear();
    }
}