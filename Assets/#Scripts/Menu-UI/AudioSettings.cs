using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    // Set volume (value between 0 and 1)
    void SetMasterVolume(float volume)
    {
        // Convert linear volume (0 to 1) to logarithmic scale
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);
    }
    public void OnSliderValueChanged(float value)
    {
        SetMasterVolume(value);
    }
}
