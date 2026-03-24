using UnityEngine;
using System.Collections;

public class SuperBallReward : MonoBehaviour
{
    [SerializeField] private float duration = 10f;

    private bool active;
    private Coroutine routine;

    public void ActivateReward(string _)
    {
        if (active) return;

        active = true;
        Debug.Log("[SuperBallReward] ACTIVATED");

        routine = StartCoroutine(Timer());
    }

    public void ResetReward()
    {
        if (!active) return;

        active = false;
        Debug.Log("[SuperBallReward] DEACTIVATED");

        if (routine != null)
            StopCoroutine(routine);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        ResetReward();
    }

    public bool IsActive() => active;
}