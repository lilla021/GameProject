using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octo : MonoBehaviour
{
    [SerializeField]
    float mForce;

    [SerializeField]
    //float mOctoSpeed;

    Rigidbody2D octo;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        octo = GetComponent<Rigidbody2D>();
        startPosition = octo.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(0f, Time.deltaTime, 0f);
        if (octo.transform.position.y <= startPosition.y)  //makes player jump
        {

            octo.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            
        }
    }

   
}
