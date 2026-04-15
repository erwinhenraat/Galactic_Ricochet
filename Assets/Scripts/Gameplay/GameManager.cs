using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action onReadyToRestart;
    [SerializeField] private TextAsset loadFile;
    private bool waitForHighScoreInitials = false;
    [SerializeField] private CrosshairInput crosshairInput;
    [SerializeField] private Aim aim;
    [SerializeField] private GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        Scene tempScene = SceneManager.GetActiveScene();
        if (tempScene.name == "Loaded_Scene") Load();
        Score.onHighScoreBrokenAtPlay += OnHighScoreBroken;
        Lives.onGameOver += OnGameOver;

       // crosshairInput = FindAnyObjectByType<CrosshairInput>();
       // aim = FindAnyObjectByType<Aim>();
    }
    private void OnDisable()
    {
        Score.onHighScoreBrokenAtPlay -= OnHighScoreBroken;
        Lives.onGameOver -= OnGameOver;
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
    void Load()
    {
        if (loadFile == null)
        {
            Debug.LogWarning("No load file assigned.");
            return;
        }

        string json = loadFile.text;
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(json);

        foreach (SavedObject data in saveFile.objects)
        {
            GameObject obj = Instantiate(prefab);
            obj.tag = data.tag;
            obj.layer = data.layer;
            obj.transform.position = data.position;
        }

        Debug.Log("Loaded");
    }
}
