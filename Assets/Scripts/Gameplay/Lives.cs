using System;
using UnityEngine;

public class Lives : MonoBehaviour
{

    public static event Action<string> onGameOver;
    public static event Action onDepleted;
    public static event Action onReload;


    [SerializeField] private int lives = 5;
    private int shotsLeft = 0;


    public int LivesLeft { get => lives; }
    public int ShotsLeft { get => shotsLeft; }
    void Awake()
    {
        shotsLeft = lives;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayArea.onBallLost += LoseLife;
        HazardObject.onBallDestroyed += LoseLife;
        Shoot.onShootNewBall += LoseShot;
        ExtraBall.onExtraBall += AddShotAndLife;


    }
    private void OnDisable()
    {
        PlayArea.onBallLost -= LoseLife;
        HazardObject.onBallDestroyed -= LoseLife;
        Shoot.onShootNewBall -= LoseShot;
        ExtraBall.onExtraBall -= AddShotAndLife;
    }
    private void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            onGameOver?.Invoke("Game Over !");
        }
    }
    private void LoseShot(GameObject _)
    {
        shotsLeft--;
        if (shotsLeft <= 0)
        {
            onDepleted?.Invoke();
        }
    }
    private void AddShotAndLife(string _)
    {
        shotsLeft++;
        lives++;
        if (shotsLeft > 0) onReload?.Invoke();

    }


}
