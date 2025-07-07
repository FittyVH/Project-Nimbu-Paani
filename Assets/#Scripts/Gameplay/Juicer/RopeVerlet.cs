using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVerlet : MonoBehaviour
{
    [Header("Rope Segments")]
    [SerializeField] int ropeSegmentsNum = 50;
    [SerializeField] float ropeSegmentsLen = 0.225f;

    [Header("Physics")]
    [SerializeField] Vector2 gravityForce = new Vector2(0f, -2f);
    [SerializeField] float dampingFactor = 0.98f;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float collisionRadius = 0.1f;
    [SerializeField] float bounceFactor = 0.1f;
    [SerializeField] float correctionClampAmount = 0.1f;

    [Header("Constraints")]
    [SerializeField] int constraintsNum = 50;


    [SerializeField] int collisionSegmentInterval = 2;

    LineRenderer lineRenderer;
    List<RopeSegment> ropeSegmentsList = new List<RopeSegment>();

    Vector3 ropeStartPos; // juicer position
    [SerializeField] GameObject juicerEnd;

    void Awake()
    {
        lineRenderer.GetComponent<LineRenderer>();
        lineRenderer.positionCount = ropeSegmentsNum; // how many pos line renderer has

        ropeStartPos = juicerEnd.transform.position;

        for (int i = 0; i < ropeSegmentsNum; i++) // fill the ropeSegmentList, aka make the line
        {
            ropeSegmentsList.Add(new RopeSegment(ropeStartPos));
            ropeStartPos.y -= ropeSegmentsLen; // new start pos for each segment
        }
    }

    private void Update()
    {
        DrawRope();
    }

    private void FixedUpdate()
    {
        Simulate();

        for (int i = 0; i < constraintsNum; i++)
        {
            ApplyConstraints();

            if (i % collisionSegmentInterval == 0)
            {
                HandleCollisions();
            }
        }
    }

    void DrawRope()
    {
        // actually drawing it on the screen
        Vector3[] ropePositions = new Vector3[ropeSegmentsNum];
        for (int i = 0; i < ropeSegmentsList.Count; i++)
        {
            ropePositions[i] = ropeSegmentsList[i].currentPos;
        }
        lineRenderer.SetPositions(ropePositions);
    }

    void Simulate()
    {
        for (int i = 0; i < ropeSegmentsList.Count; i++)
        {
            RopeSegment segment = ropeSegmentsList[i];
            Vector2 velocity = (segment.currentPos - segment.oldPos) * dampingFactor;

            segment.oldPos = segment.currentPos;
            segment.currentPos += velocity;
            segment.currentPos += gravityForce * Time.fixedDeltaTime;
            ropeSegmentsList[i] = segment;
        }
    }

    void ApplyConstraints()
    {
        RopeSegment firstSegment = ropeSegmentsList[0];
        firstSegment.currentPos = juicerEnd.transform.position;
        ropeSegmentsList[0] = firstSegment;

        for (int i = 0; i < ropeSegmentsNum - 1; i++)
        {
            RopeSegment currentSeg = ropeSegmentsList[i];
            RopeSegment nextSeg = ropeSegmentsList[i + 1];

            float distance = (currentSeg.currentPos - nextSeg.currentPos).magnitude;
            float difference = distance - ropeSegmentsLen;

            Vector2 changeDir = (currentSeg.currentPos - nextSeg.currentPos).normalized;
            Vector2 changeVector = changeDir * difference;

            if (i != 0)
            {
                currentSeg.currentPos -= changeVector * 0.5f;
                nextSeg.currentPos += changeVector * 0.5f;
            }
            else
            {
                nextSeg.currentPos += changeVector;
            }

            ropeSegmentsList[i] = currentSeg;
            ropeSegmentsList[i] = nextSeg;
        }
    }

    void HandleCollisions()
    {
        for (int i = 1; i < ropeSegmentsList.Count; i++)
        {
            RopeSegment segment = ropeSegmentsList[i];
            Vector2 velocity = segment.currentPos - segment.oldPos;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(segment.currentPos, collisionRadius, collisionMask);

            foreach (Collider2D collider in colliders)
            {
                Vector2 closestPoint = collider.ClosestPoint(segment.currentPos);
                float distance = Vector2.Distance(segment.currentPos, closestPoint);

                if (distance < collisionRadius)
                {
                    Vector2 normal = (segment.currentPos - closestPoint).normalized;
                    if (normal == Vector2.zero)
                    {
                        normal = segment.currentPos - ((Vector2)collider.transform.position).normalized;
                    }

                    float depth = collisionRadius - distance;
                    segment.currentPos += normal * depth;

                    velocity = Vector2.Reflect(velocity, normal) * bounceFactor;
                }
            }

            segment.oldPos = segment.currentPos - velocity;
            ropeSegmentsList[i] = segment;
        }
    }

    public struct RopeSegment
    {
        public Vector2 currentPos;
        public Vector2 oldPos;

        public RopeSegment(Vector2 pos)
        {
            currentPos = pos;
            oldPos = pos;
        }
    }
}
