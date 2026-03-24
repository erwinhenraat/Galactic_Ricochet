using UnityEngine;

public class SuperBallMarker : MonoBehaviour
{
    public bool IsSuperBall { get; private set; }

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Enable()
    {
        IsSuperBall = true;

        // Make ball bigger
        transform.localScale = originalScale * 1.5f;

        Debug.Log("[SuperBallMarker] Ball is now SUPER BALL");
    }

    public void Disable()
    {
        IsSuperBall = false;

        // Restore original size
        transform.localScale = originalScale;

        Debug.Log("[SuperBallMarker] Ball returned to NORMAL");
    }
}