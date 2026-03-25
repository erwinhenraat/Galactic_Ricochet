using UnityEngine;
using UnityEngine.Splines;

public class RailController : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float checkRadius;
    [SerializeField] private GameObject arrow;
    private bool hasSeenBall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spline = GetComponent<SplineContainer>();
        SetArrow();
    }


    private void SetArrow()
    {
        Vector3 pointFirst = spline.Spline.ToArray()[0].Position;
        arrow.transform.position = pointFirst;
    }

    // Update is called once per frame
    void Update()
    {
        checkForBall();
    }

    private void checkForBall()
    {
        Vector3 pointFirst = spline.Spline.ToArray()[0].Position;
        Collider2D col = Physics2D.OverlapCircle(pointFirst, checkRadius);
        if (col != null && col.CompareTag("Ball") && hasSeenBall == false)
        {
            Vector3 pointLast = spline.Spline.ToArray()[spline.Spline.Count - 1].Position;
            Vector3 pointSecondToLast = spline.Spline.ToArray()[spline.Spline.Count - 2].Position;
            col.gameObject.GetComponent<BallToRailConnector>().onIsOnRail.Invoke(this.GetComponent<SplineContainer>(),(pointLast-pointSecondToLast).normalized);
            hasSeenBall = true;
        }
        if (col == null || col.CompareTag("Ball") == false)
        {
            hasSeenBall = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(spline != null)
        {
            Vector3 pointFirst = spline.Spline.ToArray()[0].Position;
            Gizmos.DrawWireSphere(pointFirst, checkRadius);
        }
        this.transform.position = Vector3.zero;
    }
}
