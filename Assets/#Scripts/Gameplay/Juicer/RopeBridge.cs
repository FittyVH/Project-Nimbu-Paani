using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBridge : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float ropeSegLen = 0.25f;
    private int segmentLength = 35;
    private float lineWidth = 0.1f;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        float maxRopeLength = ropeSegLen * (segmentLength - 1);
        float endToStartDist = Vector2.Distance(StartPoint.position, EndPoint.position);

        // Clamp EndPoint if it's pulled too far from StartPoint
        if (endToStartDist > maxRopeLength)
        {
            Vector2 dir = (EndPoint.position - StartPoint.position).normalized;
            EndPoint.position = StartPoint.position + (Vector3)(dir * maxRopeLength);
        }

        // Fix start segment to StartPoint
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = StartPoint.position;
        ropeSegments[0] = firstSegment;

        // Fix end segment to EndPoint
        RopeSegment lastSegment = ropeSegments[ropeSegments.Count - 1];
        lastSegment.posNow = EndPoint.position;
        ropeSegments[ropeSegments.Count - 1] = lastSegment;

        // Adjust internal segments to maintain segment length
        for (int i = 0; i < segmentLength - 1; i++)
        {
            RopeSegment segA = ropeSegments[i];
            RopeSegment segB = ropeSegments[i + 1];

            float dist = (segA.posNow - segB.posNow).magnitude;
            float error = dist - ropeSegLen;

            Vector2 changeDir = (segA.posNow - segB.posNow).normalized;
            Vector2 changeAmount = changeDir * error;

            if (i != 0)
            {
                segA.posNow -= changeAmount * 0.5f;
                segB.posNow += changeAmount * 0.5f;
            }
            else
            {
                segB.posNow += changeAmount; // First segment is fixed
            }

            ropeSegments[i] = segA;
            ropeSegments[i + 1] = segB;
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
