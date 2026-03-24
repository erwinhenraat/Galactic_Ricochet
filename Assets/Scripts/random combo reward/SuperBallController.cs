using UnityEngine;

public class SuperBallController : MonoBehaviour
{
    private SuperBallReward reward;
    private SuperBallMarker marker;

    private void Start()
    {
        reward = FindObjectOfType<SuperBallReward>();
        marker = GetComponent<SuperBallMarker>();
    }

    private void Update()
    {
        if (reward == null || marker == null) return;

        if (SuperBallReward.IsSuperBallActive && !marker.IsSuperBall)
            marker.Enable();
        else if (!SuperBallReward.IsSuperBallActive && marker.IsSuperBall)
            marker.Disable();
    }
}