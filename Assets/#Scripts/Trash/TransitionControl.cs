using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionControl : MonoBehaviour
{
    private Animator transitionAnimator;

    void Start()
    {
        transitionAnimator = GetComponent<Animator>();
        transitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void Update()
    {
        if (Time.timeScale == 0f)
    {
        transitionAnimator.Update(Time.unscaledDeltaTime);
    }
    }
}
