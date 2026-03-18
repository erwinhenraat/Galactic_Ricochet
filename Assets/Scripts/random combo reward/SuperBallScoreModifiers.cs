using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;

    private const int SUPER_BALL_MULTIPLIER = 10; // Changed from 100 → 10

    private void OnEnable()
    {
        Debug.Log("[ScoreManager] Enabled");
        HitBumper.onHitBumper += OnBumperHit;
    }

    private void OnDisable()
    {
        Debug.Log("[ScoreManager] Disabled");
        HitBumper.onHitBumper -= OnBumperHit;
    }

    private void OnBumperHit(Transform bumper, int basePoints)
    {
        int finalPoints = basePoints;

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            SuperBallMarker sb = ball.GetComponent<SuperBallMarker>();
            if (sb != null && sb.IsSuperBall)
            {
                Debug.Log($"[ScoreManager] Super Ball detected → applying {SUPER_BALL_MULTIPLIER}x multiplier");
                finalPoints *= SUPER_BALL_MULTIPLIER;
            }
        }

        Debug.Log($"[ScoreManager] Adding score: {finalPoints}");
        AddScore(finalPoints);
    }

    private void AddScore(int points)
    {
        score += points;
        Debug.Log($"[ScoreManager] TOTAL SCORE: {score}");
    }
}