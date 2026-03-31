using UnityEngine;
using UnityEngine.UI;

public class RGBui : MonoBehaviour
{
    [Tooltip("Speed at which the hue cycles (1 = full loop per second)")]
    [SerializeField] private float hueSpeed = 0.2f;

    [Tooltip("Saturation for the generated color (0-1)")]
    [Range(0f, 1f)][SerializeField] private float saturation = 1f;

    [Tooltip("Brightness/value for the generated color (0-1)")]
    [Range(0f, 1f)][SerializeField] private float value = 1f;

    [Tooltip("If true uses unscaled time (ignores Time.timeScale)")]
    [SerializeField] private bool useUnscaledTime = false;

    [Tooltip("Preserve the image's original alpha channel")]
    [SerializeField] private bool preserveAlpha = true;

    [Tooltip("Start cycling immediately")]
    [SerializeField] private bool playOnStart = true;

    private Image _image;
    private float _startHue = 0f;
    private float _originalAlpha = 1f;
    private bool _isPlaying;

    private void Awake()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.LogWarning($"RGBui on '{gameObject.name}' expects an Image component on the same GameObject.");
            enabled = false;
            return;
        }

        _originalAlpha = _image.color.a;
    }

    private void Start()
    {
        _isPlaying = playOnStart;
        _startHue = Random.value;
    }

    private void Update()
    {
        if (!_isPlaying || _image == null) return;

        float t = useUnscaledTime ? Time.unscaledTime : Time.time;
        float hue = (_startHue + t * hueSpeed) % 1f;

        Color rgb = Color.HSVToRGB(hue, saturation, value);
        if (preserveAlpha) rgb.a = _originalAlpha;

        _image.color = rgb;
    }

    
    public void Play() => _isPlaying = true;
    public void Stop() => _isPlaying = false;
    public void Toggle() => _isPlaying = !_isPlaying;
}