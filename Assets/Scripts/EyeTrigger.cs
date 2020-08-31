using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrigger : MonoBehaviour
{
    [SerializeField] float time = .5f;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] AnimationCurve curve = null;
    [SerializeField] GameObject disableObject = null;
    [SerializeField] GameObject eyeObject = null;

    float startTime = 0;
    Vector3 originalPosition;

    void OnEnable()
    {
        originalPosition = eyeObject.transform.localPosition;
        startTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(time == 0)
        {
            return;
        }
        time -= Time.deltaTime;
        time = Math.Max(0, time);
        eyeObject.transform.localPosition = originalPosition + offset * curve.Evaluate(1 - time / startTime);
        if(time == 0)
        {
            disableObject.SetActive(false);
        }
    }
}
