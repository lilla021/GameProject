using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Transform newPos;
    public Rigidbody2D triggerBody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerBody == null)
        {
            return;
        }

        if (collision.attachedRigidbody == triggerBody)
        {
            collision.transform.position = newPos.position;
        }
    }
}
