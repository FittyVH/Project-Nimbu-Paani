using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDiagonal : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    Vector2 direction;

    void Start()
    {
        direction = new Vector2(-1f, 1f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
