using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTop : MonoBehaviour
{
    [Header("references")]
    [SerializeField] GameObject connectionCollider;

    [Header("audio references")]
    [SerializeField] AudioClip beepSound;
    [SerializeField] AudioSource audioSource;

    [Header("Sprite & material")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Material darkShader;
    [SerializeField] Material defaltShader;

    private Rigidbody2D rb;
    private Camera cam;
    private Vector3 offset;
    private float savedGravityScale;
    private RigidbodyConstraints2D originalConstraints;

    public bool isPlugged = false;
    bool isDragging = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        originalConstraints = rb.constraints;
    }

    private void OnMouseDown()
    {
        isDragging = true;

        savedGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        offset = transform.position - GetMouseWorldPosition();

        if (isPlugged)
        {
            connectionCollider.GetComponent<BoxCollider2D>().enabled = false;

            // resume motion
            isDragging = false;
            rb.gravityScale = savedGravityScale;
            rb.velocity = Vector2.zero;
            rb.constraints = originalConstraints;
            rb.bodyType = RigidbodyType2D.Dynamic;

            // re-enable connection collider
            Invoke(nameof(EnableConnectionCollider), 0.2f);
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            Vector2 direction = (targetPosition - transform.position) / Time.fixedDeltaTime;
            rb.velocity = direction;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        rb.gravityScale = savedGravityScale;
        rb.velocity = Vector2.zero;
        rb.constraints = originalConstraints;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert mouse position to world position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        return cam.ScreenToWorldPoint(mousePos);
    }

    void EnableConnectionCollider()
    {
        connectionCollider.GetComponent<BoxCollider2D>().enabled = true;
        isPlugged = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == connectionCollider)
        {
            // stop plug motion
            savedGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.bodyType = RigidbodyType2D.Static;

            // play beep sound
            audioSource.clip = beepSound;
            audioSource.Play();

            isPlugged = true;
        }
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
