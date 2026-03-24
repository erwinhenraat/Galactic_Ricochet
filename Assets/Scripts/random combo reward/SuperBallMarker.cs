using UnityEngine;

public class SuperBallMarker : MonoBehaviour
{
    public bool IsSuperBall { get; private set; }

    private Vector3 originalScale;
    private bool initialized;

    private void Awake()
    {
        originalScale = transform.localScale;
        initialized = true;

        Debug.Log($"[SuperBallMarker] Initialized with scale: {originalScale}");
    }

    public void Enable()
    {
        if (!initialized)
        {
            Debug.LogWarning("[SuperBallMarker] Tried to enable before initialization");
            return;
        }

        IsSuperBall = true;

        Vector3 newScale = originalScale * 1.5f;
        transform.localScale = newScale;

        Debug.Log($"[SuperBallMarker] Enable() CALLED → Scale set to {newScale}");
    }

    public void Disable()
    {
        if (!initialized)
        {
            Debug.LogWarning("[SuperBallMarker] Tried to disable before initialization");
            return;
        }

        IsSuperBall = false;

        transform.localScale = originalScale;

        Debug.Log($"[SuperBallMarker] Disable() CALLED → Scale reset to {originalScale}");
    }
}