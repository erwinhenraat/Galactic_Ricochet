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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    private void RetreiveHighScore() {
        //get highscore
        highscore = PlayerPrefs.GetInt("highscore");
        string holder = PlayerPrefs.GetString("holder");
        highScoreField.text = $"Highest by {holder} : {highscore}";
    }
    private void Update()
    {
       ResetHighScore();
    }
    private void ResetHighScore() {
        if (Input.GetKeyDown(KeyCode.Backspace) )
        {
            Debug.LogWarning("Resetting Highscore!!");
            highscore = 0;
            highScoreField.text = $" ";
            PlayerPrefs.SetInt("highscore", 0);//254110
            //PlayerPrefs.SetString("holder", "SPE");
        }
    }

   

    private void GetScore(Transform bumper , int baseScore) {
        int addedScore = baseScore * scoreMultiplier;
        value += addedScore;
        onGetScore?.Invoke((Vector2)bumper.position, addedScore);
        ShowScore();
        
        //check highscore during play
        if (value > highscore) { 
            onHighScoreBrokenAtPlay.Invoke();
        }
        

    }
    private void ShowScore() { 
        textfield.text = "Score : "+value.ToString();
    }
    private void SetMultiplier(int value) { 
        scoreMultiplier = value;
    }
    private void CheckForHighScore(string _)
    {

        //Debug.Log($"checking highscore score:{value} -- high{highscore}");
        //check only on game over!
        if (value > highscore)
        {
            //Debug.Log("highscore found");
            highscore = value;
            
            onSaveNewHighscore?.Invoke(highscore);
         
        }

    }
    private void SaveHighScore(string holder) {
        //save highscore
        //Debug.Log("saving highscore");
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.SetString("holder", holder);


    }
}
