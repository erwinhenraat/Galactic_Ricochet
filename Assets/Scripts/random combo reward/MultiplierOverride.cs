using UnityEngine;

public class MultiplierOverride : MonoBehaviour
{
    private Multiplier multiplier;

    private void Start()
    {
        multiplier = FindObjectOfType<Multiplier>();

        if (multiplier == null)
        {
            Debug.LogError("[MultiplierOverride] Multiplier script not found!");
        }
    }

    private void Update()
    {
        if (multiplier == null) return;

        if (SuperBallReward.IsSuperBallActive)
        {
            ForceMultiplier(10);
        }
    }

    private void ForceMultiplier(int value)
    {
        var field = typeof(Multiplier).GetField(
            "value",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
        );

        if (field != null)
        {
            field.SetValue(multiplier, value);
        }
    }
}