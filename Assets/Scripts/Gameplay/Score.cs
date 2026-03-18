using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public static event Action<Vector2, int> onGetScore;
    public static event Action<int> onSaveNewHighscore;
    public static event Action onHighScoreBrokenAtPlay;

    private int value = 0;
    private TMP_Text textfield;
    private int scoreMultiplier = 1;
    private int highscore;    
    public TMP_Text highScoreField;

    void Start()
    {
        HitBumper.onHitBumper += GetScore;
        Multiplier.onMultiplierUpdate += SetMultiplier;
        Lives.onGameOver += CheckForHighScore;
        SelectInitials.onInitialsSubmitted += SaveHighScore;
        
        textfield = GetComponent<TMP_Text>();

        RetreiveHighScore();
    }

    private void OnDisable()
    {
        HitBumper.onHitBumper -= GetScore;
        Multiplier.onMultiplierUpdate -= SetMultiplier;
        Lives.onGameOver -= CheckForHighScore;
        SelectInitials.onInitialsSubmitted -= SaveHighScore;
    }

    private void RetreiveHighScore()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        string holder = PlayerPrefs.GetString("holder");
        highScoreField.text = $"Highest by {holder} : {highscore}";
    }

    private void Update()
    {
       ResetHighScore();
    }

    private void ResetHighScore()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.LogWarning("Resetting Highscore!!");
            highscore = 0;
            highScoreField.text = $" ";
            PlayerPrefs.SetInt("highscore", 0);
        }
    }

    private void GetScore(Transform bumper , int baseScore)
    {
        int addedScore = baseScore * scoreMultiplier;

        // Check if the current ball is a Super Ball
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            SuperBallMarker sb = ball.GetComponent<SuperBallMarker>();

            if (sb != null && sb.IsSuperBall)
            {
                Debug.Log("[Score] Super Ball detected → applying 10x bonus");
                addedScore *= 10; // Apply a 10x multiplier for Super Ball hits
            }
        }

        value += addedScore;

        onGetScore?.Invoke((Vector2)bumper.position, addedScore);

        ShowScore();
        
        if (value > highscore)
        { 
            onHighScoreBrokenAtPlay?.Invoke();
        }
    }

    private void ShowScore()
    { 
        textfield.text = "Score : " + value.ToString();
    }

    private void SetMultiplier(int value)
    { 
        scoreMultiplier = value;
    }

    private void CheckForHighScore(string _)
    {
        if (value > highscore)
        {
            highscore = value;
            onSaveNewHighscore?.Invoke(highscore);
        }
    }

    private void SaveHighScore(string holder)
    {
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.SetString("holder", holder);
    }
}