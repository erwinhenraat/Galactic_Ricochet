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

        if (SuperBallReward.IsSuperBallActive)
        {
            transform.localScale = originalScale * 1.3f;
        }
        else
        {
            transform.localScale = originalScale;
            Debug.Log("[SuperBallMarker] Super Ball is inactive → Marker NORMAL");
        }
    }
}