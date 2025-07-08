using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;
    private float savedGravityScale;
    private RigidbodyConstraints2D originalConstraints;

    // script references
    [Header("script references")]
    [SerializeField] Telepathy telepathy;

    [Header("Sprite & material")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Material darkShader;
    [SerializeField] Material defaltShader;

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
            // Target position with offset
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            // velocity needed to move the sprite
            Vector2 direction = (targetPosition - transform.position) / Time.fixedDeltaTime;
            rb.velocity = direction; // Apply velocity to Rigidbody2D
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

    void OnMouseOver()
    {
        sprite.material = darkShader;
    }
    void OnMouseExit()
    {
        sprite.material = defaltShader;
    }
}