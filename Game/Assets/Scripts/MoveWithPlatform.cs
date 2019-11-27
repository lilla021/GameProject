using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            
            transform.parent = col.transform.parent;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
