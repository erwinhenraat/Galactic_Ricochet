using System;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    [SerializeField] private Transform flipperTransform;
    [SerializeField] private Transform flipperCollider;
    [SerializeField] private float startAngle = -30;
    [SerializeField] private float endAngle = 30;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float flipperForce = 5f;
    private bool isFlipping = false;
    private Vector3 targetRot;
    public static Action<bool> onFlipperPlaySound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRot = new Vector3(0, transform.eulerAngles.y,0);
        flipperCollider.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, -startAngle);
    }

    // Update is called once per frame
    void Update()
    {
        flipperTransform.rotation = Quaternion.Euler(targetRot);
        if(targetRot.z < -endAngle + 5)
        {
            ResetFlip();
        }
    }

    private void FixedUpdate()
    {
        if (isFlipping)
        {
            targetRot = Vector3.Lerp(targetRot, new Vector3(0, transform.eulerAngles.y, -endAngle), moveSpeed);
        }
        else
        {
            targetRot = Vector3.Lerp(targetRot, new Vector3(0, transform.eulerAngles.y, -startAngle), moveSpeed);
        }
    }

    public void CheckCollision(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Flip();
            collision.GetComponent<Rigidbody2D>().AddForce(flipperCollider.up * flipperForce, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        isFlipping = true;
        onFlipperPlaySound.Invoke(true);
    }

    private void ResetFlip()
    {
        isFlipping = false;
    }
}
