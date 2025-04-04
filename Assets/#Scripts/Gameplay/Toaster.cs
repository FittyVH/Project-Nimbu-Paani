using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField] GameObject breadCube;
    [SerializeField] Transform spawnPos;

    [SerializeField] float speed = 10f;

    [SerializeField] WireTop wireTop;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wireTop.isPlugged)
        {
            if (other.collider.tag == "Bread")
            {
                Debug.Log("TOUCH");
                Destroy(other.gameObject);
                GameObject spawnedCube = Instantiate(breadCube, spawnPos.position, spawnPos.rotation);
                Rigidbody2D rb = spawnedCube.GetComponent<Rigidbody2D>();

                rb.AddForce(spawnedCube.transform.up * speed, ForceMode2D.Impulse);
            }
        }
    }
}
