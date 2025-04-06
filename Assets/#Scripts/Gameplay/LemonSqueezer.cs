using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSqueezer : MonoBehaviour
{
    public GameObject lemonCube;
    public Transform spawnPos;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Lemon")
        {
            Destroy(other.gameObject);
            Instantiate(lemonCube, spawnPos.position, spawnPos.rotation);
        }
    }
}
