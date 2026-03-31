using System.Collections;
using TMPro;
using UnityEngine;
public class ScorePop : MonoBehaviour
{
    private float maxScale;
    private float startScale;
    private float currentScale;
    private float scaleDiff;
    private TMP_Text textfield;
    [SerializeField] float seconds;

    private bool _priority = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        Score.onGetScore += Pop;
        ExtraBall.onExtraBall += PopMessage;
        CrosshairInput.onSwapControls += PopMessage;
        Restart.onRestart += PopMessage;
        Lives.onGameOver += PopMessage;

        textfield = GetComponent<TMP_Text>();
        textfield.text = string.Empty;
    }
    private void OnDisable()
    {
        Score.onGetScore -= Pop;
        ExtraBall.onExtraBall -= PopMessage;
        CrosshairInput.onSwapControls -= PopMessage;
        Restart.onRestart -= PopMessage;
        Lives.onGameOver -= PopMessage;
    }
    private void Pop(Vector2 location, int value) {
        if (!_priority)
        {
            maxScale = 4f;
            startScale = 1f;
            currentScale = startScale;
            scaleDiff = maxScale - startScale;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(location);
            textfield.transform.position = screenPoint;
            textfield.text = string.Empty + value;
            StartCoroutine(Animate());
        }

    }
    private void PopMessage(string message) {
        _priority = true;

        maxScale = 8f;
        startScale = 1f;
        currentScale = startScale;
        scaleDiff = maxScale - startScale;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(Vector3.zero);
        textfield.transform.position = screenPoint;
        textfield.text = string.Empty + message;
        StartCoroutine(Animate());

        
    }

    private IEnumerator Animate() {
        
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
