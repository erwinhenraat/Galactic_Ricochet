using System;
using UnityEngine;
using UnityEngine.Splines;

public class BallToRailConnector : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SplineAnimate sa;
    public Action<SplineContainer,Vector2> onIsOnRail;
    public static Action<bool> onRailPlaySound;
    public static Action<bool> onRailStopSound;
    private Vector2 fireDirection;
    [SerializeField] private ParticleSystem trailParticles;
    public bool isOnRail = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sa = GetComponent<SplineAnimate>();
        onIsOnRail += SetupRailFollow;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetupRailFollow(SplineContainer spl,Vector2 shootDir)
    {
        isOnRail = true;
        fireDirection = shootDir;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;
        sa.Container = spl;
        sa.enabled = true;
        sa.MaxSpeed = rb.linearVelocity.magnitude;
        StartFollow();
        onRailPlaySound.Invoke(true);
    }

    private void StartFollow()
    {
        sa.Restart(false);
        sa.Play();
        sa.Completed += ResetIsOnRail;
        trailParticles.Play();
    }

    private void ResetIsOnRail()
    {
        isOnRail = false;
        trailParticles.Stop();
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        sa.enabled = false;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(fireDirection * sa.MaxSpeed, ForceMode2D.Impulse);
        onRailStopSound.Invoke(true);
        sa.Completed -= ResetIsOnRail;
    }
}
