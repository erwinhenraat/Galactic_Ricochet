using UnityEngine;
using System;


public class HazardWarning : MonoBehaviour
{
    public static event Action onHazardWarning;

    [SerializeField]private float timer = 0f;
    private float spawnTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private bool visibility = false;
    void Start()
    {
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
        spawnTimer += Time.deltaTime;

        if (spawnTimer > 4f) {
            onHazardWarning?.Invoke();
            spawnTimer = 0f;
        }

        if (timer < 0.5) return;
        visibility = !visibility;
        timer = 0;
    }


    void Update()
    {
        if (visibility == true)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
}
