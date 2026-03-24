using UnityEngine;

public class SuperBallMarker : MonoBehaviour
{
        private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }
    private void Update()
    {
        // Check the global SuperBall state every frame
        if (SuperBallReward.IsSuperBallActive)
        {
            transform.localScale = originalScale * 1.5f;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}