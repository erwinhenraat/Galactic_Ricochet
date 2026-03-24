using System;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public static event Action onBallLost;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            if (collision.GetComponent<BallToRailConnector>() != null && collision.GetComponent<BallToRailConnector>().isOnRail == false)
            {
                onBallLost?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}
