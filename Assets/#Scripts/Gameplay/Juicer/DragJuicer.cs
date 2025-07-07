using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragJuicer : MonoBehaviour
{
    [Header("Rope Constraint")]
    [SerializeField] Transform otherRopeEnd;
    [SerializeField] float maxRopeLength = 8f; // Should match ropeSegLen × segmentLength

    private Rigidbody2D rb;
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;
    private float savedGravityScale;
    private RigidbodyConstraints2D originalConstraints;

    // script references
    [Header("script references")]
    [SerializeField] Telepathy telepathy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main; // Cache the main camera
        originalConstraints = rb.constraints; // initial Rigidbody2D constraints saved
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            //rotations
            if (telepathy.telepathyOn && Input.GetKey(KeyCode.A))
            {
                rb.rotation += 1f;
            }
            if (telepathy.telepathyOn && Input.GetKey(KeyCode.D))
            {
                rb.rotation -= 1f;
            }
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;

        // Save gravity
        savedGravityScale = rb.gravityScale;

        //freeze
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Calculate the offset to maintain the relative position
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorld = GetMouseWorldPosition();
            Vector3 targetPosition = mouseWorld + offset;

            // Clamp distance between this and the other end of the rope
            if (otherRopeEnd != null)
            {
                float currentDistance = Vector3.Distance(targetPosition, otherRopeEnd.position);
                if (currentDistance > maxRopeLength)
                {
                    // Clamp target to rope max length
                    Vector3 direction = (targetPosition - otherRopeEnd.position).normalized;
                    targetPosition = otherRopeEnd.position + direction * maxRopeLength;
                }
            }

            Vector2 velocity = (targetPosition - transform.position) / Time.fixedDeltaTime;
            rb.velocity = velocity;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // restore everything
        rb.gravityScale = savedGravityScale;
        rb.velocity = Vector2.zero;
        rb.constraints = originalConstraints;
        telepathy.telepathyOn = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // mouse position to world position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        return cam.ScreenToWorldPoint(mousePos);
    }
}
