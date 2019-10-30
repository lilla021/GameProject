using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    public bool facingRight = true;
    public static bool follow;


    float mArriveThreshold = 0.05f;
    void Start()
    {
    }
      
    void Update()
    {

       
        if (mTarget != null)
        {
            // TODO: Make the enemy follow the target "mTarget"
            //       only if the target is close enough (distance smaller than "mFollowRange")
            
            Vector2 direction = mTarget.transform.position - transform.position;
            if (direction.magnitude <= mFollowRange)
            {         
                follow = true;
                if (direction.magnitude > mArriveThreshold)
                {              
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.position = mTarget.transform.position;
                    follow = false;
                }
            }
           
            
            if (Vector3.Distance(mTarget.position, transform.position) < mFollowRange)
            {

                transform.position = Vector2.MoveTowards(transform.position, mTarget.position, mFollowSpeed * Time.deltaTime);
                if (mTarget.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                    Flip();
                if (mTarget.position.x < transform.position.x && facingRight)
                    Flip();
            }
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
