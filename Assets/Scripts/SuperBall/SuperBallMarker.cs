using UnityEngine;

public class SuperBallMarker : MonoBehaviour
{
    private Vector3 originalScale;
    private Renderer ballRenderer;
    private Color originalColor;

    private void Awake()
    {
        originalScale = transform.localScale;

        // Get the Renderer component
        ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            originalColor = ballRenderer.material.color; // store the original color
        }
        else
        {
            Debug.LogWarning("[SuperBallMarker] No Renderer found on this object!");
        }
    }

    private void Update()
    {
        if (SuperBallReward.IsSuperBallActive)
        {
            transform.localScale = originalScale * 1.3f;

            if (ballRenderer != null)
                ballRenderer.material.color = Color.green; // change color to green
        }
        else
        {
            transform.localScale = originalScale;

            if (ballRenderer != null)
                ballRenderer.material.color = originalColor; // reset to original color

            Debug.Log("[SuperBallMarker] Super Ball is inactive → Marker NORMAL");
        }
    }
}