using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluggedFeedback : MonoBehaviour
{
    [SerializeField] WireTop wireTop;
    [SerializeField] SpriteRenderer switchLight;

    void Update()
    {
        if (wireTop.isPlugged)
        {
            switchLight.color = Color.green;
        }
        else
        {
            switchLight.color = Color.red;
        }
    }
}
