using System;
using System.Collections.Generic;
using UnityEngine;

public class HitBumper : MonoBehaviour
{

    [SerializeField] private int bumperValue = 50;
    private ParticleSystem ps;
    public static event Action<Transform, int> onHitBumper;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float frameTime = 0.5f;
    private bool isPlayingAnim = false;
    [SerializeField] private float holdFrameTime = 0.3f;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps?.Stop();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            onHitBumper?.Invoke(gameObject.transform, bumperValue);
            ps?.Stop();
            ps?.Play();
            isPlayingAnim = true;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayingAnim)
        {
            for (float i = 0; i < sprites.Count; i += frameTime)
            {
                this.GetComponent<SpriteRenderer>().sprite = sprites[(int)Math.Floor(i)];
                if (i == sprites.Count - 1)
                {
                    Invoke("ResetAnim", holdFrameTime);
                    isPlayingAnim = false;
                }
            }
        }

    }

    private void ResetAnim()
    {
        this.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
