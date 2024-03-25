using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Target dat gevolgd wordt
    [SerializeField] private Transform target;
    //Hoe snel de camera volgt, standaard op 0.125f
    [SerializeField]
    [Range(0.01f, 1f)]

    private float smoothSpeed = 0.125f;
    //Verschil tussen de target en de camera (x,y,z)
    [SerializeField] private Vector3 offset;

    //Standaard geen verschil tussen camera en target
    private Vector3 velocity = Vector3.zero;

    //Late update wordt na Update uitgeveoerd. Na het bewegen dus.

    private void LateUpdate()
    {
        //Waar de cameraa naartoe moet
        Vector3 desiredPosition = target.position + offset;
        //Bewegen van de camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

}
