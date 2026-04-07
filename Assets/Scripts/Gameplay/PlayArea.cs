using System;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public static event Action onBallLost;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {

            onBallLost?.Invoke();
            Destroy(collision.gameObject);


        }
    }
}
