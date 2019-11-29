using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    Vector3 rightOffset;

    [SerializeField]
    Vector3 leftOffset;

    [SerializeField]
    float minSpeed;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float speed;

    float t;
    bool increasing;

    Vector3 point1;
    Vector3 point2;

    void Start()
    {
        t = Random.value;
        increasing = true;
        speed = Random.Range(minSpeed, maxSpeed);
        point1 = transform.position + rightOffset;
        point2 = transform.position + leftOffset;

    }

    void Update()
    {
        transform.position = Vector3.Lerp(point1, point2, t);

        if (increasing)
        {
            t += Time.deltaTime * speed;
            if (t > 1.0f)
            {
                increasing = false;
            }
        }
        else
        {
            t -= Time.deltaTime * speed;
            if (t < 0.0f)
            {
                increasing = true;
            }
        }

    }

}
