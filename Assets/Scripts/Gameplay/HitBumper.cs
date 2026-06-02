using System;
using System.Collections;
using UnityEngine;

public class HitBumper : MonoBehaviour
{

    [SerializeField] private int bumperValue = 50;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private float hitDuration = 0.2f;

    private Sprite defaultSprite;

    private ParticleSystem ps;
    private SpriteRenderer sr;
    public static event Action<Transform, int> onHitBumper;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps?.Stop();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {

            onHitBumper?.Invoke(gameObject.transform, bumperValue);
            ps?.Stop();
            ps?.Play();

            sr.sprite = hitSprite;
            StartCoroutine(ResetSprite());

        }
    }
    private IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(hitDuration);
        sr.sprite = defaultSprite;
    }
}
