using UnityEngine;
using System;

public class HazardObject : MonoBehaviour
{
    public static event Action onBallDestroyed;
    private float timer = 0f;
    private float velocity;

    public float Velocity
    {
        get { return velocity; } set { velocity = value; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.position += transform.right * Time.deltaTime * velocity;

        if (timer > 4f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            onBallDestroyed?.Invoke();// Nodig voor PlaySound.cs
            Destroy(collision.gameObject);
            Debug.Log("Event Fired");
        }
    }

    
}
