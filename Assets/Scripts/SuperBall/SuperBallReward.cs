using UnityEngine;
using System.Collections;

public class SuperBallReward : MonoBehaviour
{
    [SerializeField] private float duration = 10f;

    public static bool IsSuperBallActive { get; private set; }

    private Coroutine routine;

    public void ActivateReward(string _)
    {
        if (IsSuperBallActive) return;

        IsSuperBallActive = true;

        Debug.Log("[SuperBallReward] ACTIVATED → 10x ENABLED");

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(Timer());
    }

    public void ResetReward()
    {
        if (!IsSuperBallActive) return;

        IsSuperBallActive = false;

        Debug.Log("[SuperBallReward] DEACTIVATED");

        if (routine != null)
            StopCoroutine(routine);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        ResetReward();
    }
}