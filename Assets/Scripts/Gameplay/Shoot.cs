using System;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Action<GameObject> onShootNewBall;
    public static Action onPress;
    public static Action onRelease;


    [SerializeField] private GameObject prefab;
    [SerializeField] private float forcePerSecond = 20f;
    [SerializeField] private List<Color> beamColors = new List<Color>();
    [SerializeField] private float lineStartWidth = 0.1f;
    [SerializeField] private float lineEndWidth = 0.07f;
    [SerializeField] private float maximumHoldSeconds = 5f;

    private float _pressTimer = 0f;
    private float _launchForce = 0f;

    private bool _drawLine = false;
    private bool _isEnabled = true;
    private bool _startPress = false;
    private bool _endPress = false;

    private LineRenderer lineRenderer;
    private ParticleSystem particles;  

    private void Start()
    {
        Lives.onDepleted += DisableShot;
        Lives.onReload += ReloadShot;
        CrosshairInput.onPressFire1 += HandlePressFire;
        CrosshairInput.onReleaseFire1 += HandleReleaseFire;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        if (beamColors.Count != 2)
        {
            Debug.LogWarning("You must choose only 2 colors for your beam!");
        }
        else
        {
            lineRenderer.startColor = beamColors[0];
            lineRenderer.endColor = beamColors[1];
        }

        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;

        lineRenderer.positionCount = 2;

        particles = GetComponent<ParticleSystem>();
        particles.Stop();

    }
    private void OnDisable()
    {
        Lives.onDepleted -= DisableShot;
        Lives.onReload -= ReloadShot; 
        CrosshairInput.onPressFire1 -= HandlePressFire;
        CrosshairInput.onReleaseFire1 -= HandleReleaseFire;
    }
    void Update()
    {
        if (_isEnabled) {
            HandleShot();
            DrawForceLine();
        }
    }
    void HandleShot() { 
        if (_startPress)
        { //als de knop ingedrukt word
            _pressTimer = 0; //reset de timer
            ActivateLine(true);
            particles.Play();
            onPress?.Invoke();

        }
        if (_endPress)
        { //als je de knop loslaat
            _launchForce = _pressTimer * forcePerSecond; //bepaal de kracht via de timer
            GameObject ball = Instantiate(prefab, transform.parent); //maak een bal
            ball.transform.rotation = transform.rotation;   //draai de bal in de richting van de pijl
            ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.right * _launchForce, ForceMode2D.Impulse); //geef kracht mee in de juiste richting
            ball.transform.position = transform.position; //geef de bal de positie van de pijl
            ActivateLine(false);

            //stuur de nieuwe bal mee via een Unity Event
            onShootNewBall?.Invoke(ball);
            onRelease?.Invoke();
            particles.Stop();

        }
        if(_pressTimer < maximumHoldSeconds) _pressTimer += Time.deltaTime; //houd de teller bij


        _startPress = false;
        _endPress = false;
    }
    private void ActivateLine(bool value) {
        _drawLine = value;
        lineRenderer.enabled = value;
    }

    private void DrawForceLine()
    {
        if (_drawLine)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (transform.right * _pressTimer * 5f));
        }
    }
    private void DisableShot() {
        _isEnabled = false;
    }
    private void ReloadShot()
    {
        _isEnabled = true;
    }
    private void HandlePressFire() {
        _startPress = true;
    }
    private void HandleReleaseFire() { 
        _endPress = true;
    }
}
