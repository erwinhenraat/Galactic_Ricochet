using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action onReadyToRestart;
    private bool waitForHighScoreInitials = false;
    private bool paused = false;
    [SerializeField] private CrosshairInput crosshairInput;
    [SerializeField] private Aim aim;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject backdrop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score.onHighScoreBrokenAtPlay += OnHighScoreBroken;
        Lives.onGameOver += OnGameOver;
        CrosshairInput.onPressPause += OnPause;

       // crosshairInput = FindAnyObjectByType<CrosshairInput>();
       // aim = FindAnyObjectByType<Aim>();

    }
    private void OnDisable()
    {
        Score.onHighScoreBrokenAtPlay -= OnHighScoreBroken;
        Lives.onGameOver -= OnGameOver;
    }

    private void OnPause()
    {
        if (waitForHighScoreInitials)
            return;

        if (backdrop != null)
            backdrop.SetActive(!paused);

        cannon.SetActive(paused);
        Time.timeScale = paused ? 1.0f : 0.0f;
        
        paused = !paused;
    }

    private void OnHighScoreBroken() {
        waitForHighScoreInitials = true;
    }
    private void OnGameOver(string _) {

        if (!waitForHighScoreInitials) {           

            onReadyToRestart?.Invoke();
            return;
        }
        SelectInitials.onInitialsSubmitted += OnHighScoreSubmitted;
        Debug.Log("disable crosshair input and aim");
        crosshairInput.enabled = false;
        aim.enabled = false;

    }
    private void OnHighScoreSubmitted(string _) {
        SelectInitials.onInitialsSubmitted -= OnHighScoreSubmitted;
        waitForHighScoreInitials = false;
        onReadyToRestart?.Invoke();

        crosshairInput.enabled = true;
        aim.enabled = true;
    }
}
