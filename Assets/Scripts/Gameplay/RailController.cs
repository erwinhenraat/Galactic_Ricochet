using UnityEngine;
using UnityEngine.Splines;

public class RailController : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float checkRadius;
    [SerializeField] private GameObject arrow;
    private bool hasSeenBall = false;

    void Start()
    {
        spline = GetComponent<SplineContainer>();
        SetArrow();
        SetupEntranceTrigger();
    }

    private void SetArrow()
    {
        Vector3 pointFirst = spline.Spline[0].Position;
        arrow.transform.position = pointFirst;
    }

    // Creates a physics trigger at the rail entrance so that fast-moving balls
    // are caught by Unity's continuous collision detection instead of a
    // per-frame OverlapCircle snapshot, preventing the ball from tunnelling
    // through the entrance between frames.
    private void SetupEntranceTrigger()
    {
        // Spline knot positions are in the SplineContainer's local space, which
        // is the same local space that CircleCollider2D.offset uses, so no
        // coordinate conversion is needed regardless of where the rail is placed.
        Vector2 entranceLocal = spline.Spline[0].Position;
        CircleCollider2D trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.isTrigger = true;
        trigger.radius = checkRadius;
        trigger.offset = entranceLocal;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball") && !hasSeenBall)
        {
            int lastIndex = spline.Spline.Count - 1;
            Vector3 pointLast = spline.Spline[lastIndex].Position;
            Vector3 pointSecondToLast = spline.Spline[lastIndex - 1].Position;
            col.gameObject.GetComponent<BallToRailConnector>().onIsOnRail.Invoke(
                this.GetComponent<SplineContainer>(),
                (pointLast - pointSecondToLast).normalized
            );
            hasSeenBall = true;
        }
    }

    // Disabling the ball's collider inside SetupRailFollow causes Unity to fire
    // OnTriggerExit2D automatically, which resets hasSeenBall so the next ball
    // can trigger the rail correctly.
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            hasSeenBall = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(spline != null)
        {
            Vector3 pointFirst = spline.Spline[0].Position;
            Gizmos.DrawWireSphere(pointFirst, checkRadius);
        }
        this.transform.position = Vector3.zero;
    }
}
