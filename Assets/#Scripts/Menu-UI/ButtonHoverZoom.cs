using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHoverZoom : MonoBehaviour
{
    RectTransform recttransform;
    [SerializeField] float zoomValue = 1.2f;
    [SerializeField] float zoomTime = 0.1f;
    [SerializeField] float roataionMagnitude = 10f;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    public void HoverEnter()
    {
        float randomRotation = Random.Range(-1f, 1f) * roataionMagnitude;
        recttransform.DOScale(new Vector2(zoomValue, zoomValue), zoomTime);
        recttransform.DORotate(new Vector3(0f, 0f, randomRotation), zoomTime);
    }

    public void HoverExit()
    {
        recttransform.DOScale(new Vector2(1f, 1f), zoomTime);
        recttransform.DORotate(new Vector3(0f, 0f, 0f), zoomTime);
    }
}
