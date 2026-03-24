using UnityEngine;
using System.Collections;

public class SuperBallReward : MonoBehaviour
{
    [SerializeField] private float duration = 10f;

    private bool active;

    public static bool IsSuperBallActive;

    public void ActivateReward(string _)
    {
        if (active) return;

        active = true;
        IsSuperBallActive = true;

        Debug.Log("[SuperBallReward] ACTIVATED (10x boost)");

        StartCoroutine(Timer());
    }

    public void ResetReward()
    {
        if (!active) return;

        active = false;
        IsSuperBallActive = false;

        Debug.Log("[SuperBallReward] DEACTIVATED");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        ResetReward();
    }

    public bool IsActive() => active;
}