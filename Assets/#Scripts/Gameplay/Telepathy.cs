using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Telepathy : MonoBehaviour
{

    public bool telepathyOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            telepathyOn = true;
            Debug.Log("telepathy");
        }

        if (!telepathyOn)
        {
            Debug.Log("telepathy off");
        }
    }
}
