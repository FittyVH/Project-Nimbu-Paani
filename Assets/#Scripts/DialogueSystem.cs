using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pageTurnAudio;

    public GameObject[] comic;
    int i = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && i < comic.Length - 1)
        {
            PlayAudio();
            comic[i].SetActive(false);
            i++;
            comic[i].SetActive(true);
        }    
    }

    void PlayAudio()
    {
        audioSource.clip = pageTurnAudio;
        audioSource.Play();
    }
}
