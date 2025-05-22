using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    [SerializeField] GameObject destroyParticles;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Instantiate(destroyParticles, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}
