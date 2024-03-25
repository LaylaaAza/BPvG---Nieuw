using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour
{
    public float angle = 30.0f; 
    public float speed = 2.0f; 

    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        startRotation = transform.localRotation;
        endRotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, Mathf.Sin(Time.time * speed) * 0.5f + 0.5f);
    }
}