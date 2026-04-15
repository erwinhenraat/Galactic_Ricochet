using UnityEngine;

public class FlipperCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponentInParent<FlipperController>().CheckCollision(collision.collider);
    }
}
