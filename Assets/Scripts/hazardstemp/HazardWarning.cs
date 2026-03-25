using UnityEngine;
using System;


public class HazardWarning : MonoBehaviour
{
    public static event Action onHazardWarning;

    private float timer = 0f;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        onHazardWarning?.Invoke();

        HazardSpawner.onTimerHit += WarningEffect;

        spriteRenderer = GetComponent<SpriteRenderer>();

        WarningEffect();
    }

    private void OnDisable()
    {
        HazardSpawner.onTimerHit -= WarningEffect;
    }

    private void WarningEffect() {
        timer += Time.deltaTime;

        if (timer < 0.5) return;
        //CanvasGroup.alpha = (CanvasGroup.alpha == 1f) ? 0f : 1f;
        timer = 0;
    }


    void Update()
    {
        
    }
}
