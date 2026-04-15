using UnityEngine;

public class SuperBallMarker : MonoBehaviour
{
    private Vector3 originalScale;
    private Renderer ballRenderer;
    private Color originalColor;

    [Header("Effects")]
    [SerializeField] private GameObject activationEffectPrefab; 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip activationSound;

    private ParticleSystem activationEffectInstance;
    private bool wasActiveLastFrame = false;

    private void Awake()
    {
        originalScale = transform.localScale;

        ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            originalColor = ballRenderer.material.color;
        }
    }

    private void Start()
    {
        if (SuperBallReward.IsSuperBallActive)
        {
            ActivateEffects();
            wasActiveLastFrame = true;
        }
    }

    private void Update()
    {
        bool isActive = SuperBallReward.IsSuperBallActive;

        if (isActive && !wasActiveLastFrame)
        {
            ActivateEffects();
        }

        if (!isActive && wasActiveLastFrame)
        {
            DeactivateEffects();
        }

        // Continuous visuals
        if (isActive)
        {
            transform.localScale = originalScale * 1.3f;

            if (ballRenderer != null)
                ballRenderer.material.color = Color.green;

            if (activationEffectInstance != null && !activationEffectInstance.isPlaying)
            {
                activationEffectInstance.Play();
            }
        }
        else
        {
            transform.localScale = originalScale;

            if (ballRenderer != null)
                ballRenderer.material.color = originalColor;

            if (activationEffectInstance != null && activationEffectInstance.isPlaying)
            {
                activationEffectInstance.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

        wasActiveLastFrame = isActive;
    }

    private void ActivateEffects()
    {
        Debug.Log("[SuperBallMarker] ACTIVATED on " + gameObject.name);

        // Spawn effect ONCE and attach it
        if (activationEffectInstance == null && activationEffectPrefab != null)
        {
            GameObject fx = Instantiate(activationEffectPrefab, transform);

            fx.transform.localPosition = Vector3.zero;
            fx.transform.localRotation = Quaternion.identity;

            activationEffectInstance = fx.GetComponent<ParticleSystem>();

            if (activationEffectInstance == null)
            {
                Debug.LogWarning("No ParticleSystem found on prefab root!");
            }
        }

        // Restart effect cleanly
        if (activationEffectInstance != null)
        {
            activationEffectInstance.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            activationEffectInstance.Play();
        }

        // Play sound
        if (audioSource != null && activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
        }
    }

    private void DeactivateEffects()
    {
        Debug.Log("[SuperBallMarker] DEACTIVATED on " + gameObject.name);

        if (activationEffectInstance != null)
        {
            activationEffectInstance.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}