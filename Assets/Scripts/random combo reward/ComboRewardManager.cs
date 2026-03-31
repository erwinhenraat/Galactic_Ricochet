using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ComboRewardManager : MonoBehaviour
{
    [System.Serializable]
    public class RewardData
    {
        public int requiredCombo;
        public MonoBehaviour reward;
    }

    [SerializeField] private List<RewardData> rewards;

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
        var bag = GetOrCreateBag(comboLevel);

        if (bag.Count == 0)
            return;

        var reward = bag.Dequeue();

        Debug.Log($"[ComboRewardManager] Reward chosen → {reward.name} ({reward.GetType().Name})");

       
        if (reward is ExtraBall extraBall)
        {
            Debug.Log("[ComboRewardManager] Attempting to trigger ExtraBall via reflection");

            MethodInfo method = typeof(ExtraBall).GetMethod(
                "ExtraBallCheck",
                BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (method != null)
            {
                Debug.Log("[ComboRewardManager] ExtraBallCheck found — invoking");

                method.Invoke(extraBall, new object[] { comboLevel, tag });
            }
            else
            {
                Debug.LogError("[ComboRewardManager] Failed to find ExtraBallCheck method");
            }

            return;
        }

        
        reward.SendMessage("ActivateReward", tag, SendMessageOptions.DontRequireReceiver);
    }

    private Queue<MonoBehaviour> GetOrCreateBag(int comboLevel)
    {
        if (rewardBags.ContainsKey(comboLevel) && rewardBags[comboLevel].Count > 0)
            return rewardBags[comboLevel];

        List<MonoBehaviour> bagList = new();

        foreach (var entry in rewards)
        {
            if (entry.reward != null && entry.requiredCombo == comboLevel)
            {
                bagList.Add(entry.reward);
            }
        }

        if (bagList.Count == 0)
        {
            
            return new Queue<MonoBehaviour>();
        }

      
        for (int i = 0; i < bagList.Count; i++)
        {
            int j = Random.Range(i, bagList.Count);
            (bagList[i], bagList[j]) = (bagList[j], bagList[i]);
        }

        rewardBags[comboLevel] = new Queue<MonoBehaviour>(bagList);

        Debug.Log($"[ComboRewardManager] Built bag for combo {comboLevel} with {bagList.Count} rewards");

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