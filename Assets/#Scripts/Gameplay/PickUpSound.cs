using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip pickUpSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        audioSource.clip = pickUpSound;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }
}
