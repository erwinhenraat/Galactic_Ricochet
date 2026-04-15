using UnityEngine;
using System;

public class HazardObject : MonoBehaviour
{
    public static event Action onBallDestroyed;
    private float timer = 0f;
    private float velocity;
    [SerializeField] private GameObject explosionPrefab;

    public float Velocity
    {
        get { return velocity; } set { velocity = value; }
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
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
            Destroy(explosion, 2f);
        }
    }

    
}
