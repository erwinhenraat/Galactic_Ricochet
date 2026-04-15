using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    [SerializeField] private Transform crosshairHolder;
    [SerializeField] private float rotAmount = 45f;
    [SerializeField] private float rotSpeed = 0.1f;
    [SerializeField] private float crosshairScaleMin = 0.3f;
    [SerializeField] private float crosshairScaleMax = 0.4f;
    private float crosshairScale = 0;
    private float currentRot = 0f;
    [SerializeField] private float checkRad = 1f;
    [SerializeField] private LayerMask layersToIgnore;
    private bool isOnTarget = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crosshairScale = crosshairScaleMax;
    }

 

    private void FixedUpdate()
    {
          Collider2D col = Physics2D.OverlapCircle(this.transform.position, checkRad, layersToIgnore);
        if (col)
        {
            if (col.gameObject.name.Contains("Bumper"))
            {
                isOnTarget = true;
            }
            else
            {
                isOnTarget = false;
            }
        }
        else
        {
            isOnTarget = false;
        }
        crosshairHolder.transform.rotation = Quaternion.Euler(0f, 0f, currentRot);
        crosshairHolder.transform.localScale = new Vector3(crosshairScale, crosshairScale, 1);
        if (isOnTarget)
        {
            currentRot = Mathf.Lerp(currentRot, rotAmount, rotSpeed);
            crosshairScale = Mathf.Lerp(crosshairScale, crosshairScaleMin, rotSpeed);
        }
        else
        {
            currentRot = Mathf.Lerp(currentRot, 0, rotSpeed);
            crosshairScale = Mathf.Lerp(crosshairScale, crosshairScaleMax, rotSpeed);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRad);
    }
}
