using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private float speed;

    [SerializeField] private float minSpeed = 1.0f;
    [SerializeField] private float maxSpeed = 3.0f;

    void Start()
    {
        startPosition = gameObject.transform.localPosition;
        endPosition = new Vector3(startPosition.x, startPosition.y + 5);

        // Set a random speed between minSpeed and maxSpeed
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
