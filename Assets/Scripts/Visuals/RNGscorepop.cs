using System.Collections;
using TMPro;
using UnityEngine;

public class ChromaScorePop : MonoBehaviour
{
    private float maxScale;
    private float startScale;
    private float currentScale;
    private float scaleDiff;

    private TMP_Text textfield;

    [SerializeField] float seconds;

    void Start()
    {
        Score.onGetRNGScore += RNGPop;

        textfield = GetComponent<TMP_Text>();
        textfield.text = string.Empty;
    }

    private void OnDisable()
    {
        Score.onGetRNGScore -= RNGPop;
    }

    private void RNGPop(Vector2 location, int value)
    {
        maxScale = 4f;
        startScale = 1f;
        currentScale = startScale;
        scaleDiff = maxScale - startScale;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(location);
        textfield.transform.position = screenPoint;

        textfield.text = value.ToString();

        StartCoroutine(Animate());
    }

    void Update()
    {
        if (textfield.text != string.Empty)
        {
            float hue = Mathf.PingPong(Time.time, 1f);
            textfield.color = Color.HSVToRGB(hue, 1f, 1f);
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
    }
}