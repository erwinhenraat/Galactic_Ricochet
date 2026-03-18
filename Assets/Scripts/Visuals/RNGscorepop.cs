using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class RNGscorepop : MonoBehaviour
{

    private float maxScale;
    private float startScale;
    private float currentScale;
    private float scaleDiff;
    private TMP_Text textfield;

    [SerializeField] float seconds;

    private bool isChroma = false;

    private bool _priority = false;

    private ScorePop scorepop;

    void Start()
    {
        Score.onGetRNGScore += RNGPop;
        scorepop = GetComponent<ScorePop>();
        textfield = GetComponent<TMP_Text>();
        textfield.text = string.Empty;
    }


    private void OnDisable()
    {
        Score.onGetRNGScore -= RNGPop;
    }

    private void RNGPop(Vector2 location, int value)
    {
        if (!_priority)
        {
            isChroma = true;
            textfield.text = value.ToString();
            maxScale = 4f;
            startScale = 1f;
            currentScale = startScale;
            scaleDiff = maxScale - startScale;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(location);
            textfield.transform.position = screenPoint;

            StartCoroutine(Animate());
        }
    }

    void Update()
    {
        if (isChroma && textfield.text != string.Empty)
        {
            // Maak een rainbow effect
            float hue = Mathf.PingPong(Time.time, 1f);
            textfield.color = Color.HSVToRGB(hue, 1f, 1f);
        }
        else
        {
            textfield.color = Color.white; // normale kleur
        }
    }

    private IEnumerator Animate()
    {

        while (currentScale < maxScale)
        {
            currentScale += scaleDiff / (seconds / Time.deltaTime);
            textfield.transform.localScale = Vector2.one * currentScale;
            yield return new WaitForEndOfFrame();
        }
        textfield.text = string.Empty;
        _priority = false;
    }
}
