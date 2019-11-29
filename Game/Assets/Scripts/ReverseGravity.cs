using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public Rigidbody2D triggerBody;

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (triggerBody == null)
        {
            return;
        }

        if (collision.attachedRigidbody == triggerBody && PlayerData.IsInDream)
        {
            collision.attachedRigidbody.gravityScale = -3.5f;
            collision.transform.eulerAngles = new Vector3(0, 0, 180);
            PlayerData.IsInReverseGravity = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (triggerBody == null)
        {
            return;
        }

        if (collision.attachedRigidbody == triggerBody && PlayerData.IsInDream)
        {
            collision.attachedRigidbody.gravityScale = 3.5f;
            collision.transform.eulerAngles = new Vector3(0, 0, 0);
            PlayerData.IsInReverseGravity = false;

        }
    }


}
