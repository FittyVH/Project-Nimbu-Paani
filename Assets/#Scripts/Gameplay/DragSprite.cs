using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private Rigidbody2D rb;       
    private Camera cam;               
    private bool isDragging = false;  
    private Vector3 offset;           
    private float savedGravityScale;  
    private RigidbodyConstraints2D originalConstraints; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main; // Cache the main camera
        originalConstraints = rb.constraints; // Save initial Rigidbody2D constraints
    }

    private void OnMouseDown()
    {
        isDragging = true;

        // Save gravity scale and temporarily disable gravity
        savedGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        // Freeze velocity and angular velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Temporarily freeze rotation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Calculate the offset to maintain the relative position
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // Target position with offset
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            // Calculate velocity needed to move the sprite
            Vector2 direction = (targetPosition - transform.position) / Time.fixedDeltaTime;
            rb.velocity = direction; // Apply velocity to Rigidbody2D
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // Restore gravity
        rb.gravityScale = savedGravityScale;

        // Stop movement
        rb.velocity = Vector2.zero;

        // Restore original constraints (rotation and movement)
        rb.constraints = originalConstraints;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert mouse position to world position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        return cam.ScreenToWorldPoint(mousePos);
    }
}
