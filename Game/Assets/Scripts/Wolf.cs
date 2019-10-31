using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    Rigidbody2D mRigidbody;
    Animator mAnimator;
    public float speed = 2.0f;
    bool mWalk;
    bool mRun;

    public float min = 2f;
    public float max = 3f;
    public float CurPos;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        min = transform.position.x;
        max = transform.position.x + 5f;
        CurPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowTarget.follow == true)
        {
                     
            mRun = true;
        }
        else
        {
            Move();
            mRun = false;
        }
               

        
        UpdateAnimator();
    }

    void Move()
    {
        mWalk = true;
        
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        
         if (transform.position.x > CurPos)
         {
            CurPos = transform.position.x;
            transform.localScale = new Vector2(-1, transform.localScale.y);
         }
         if (transform.position.x < CurPos) 
         {
            CurPos = transform.position.x;
            transform.localScale = new Vector2(1, transform.localScale.y);
         }
         
    }
   
    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", mWalk);
        mAnimator.SetBool("isRunning", mRun);
    }

    
}
