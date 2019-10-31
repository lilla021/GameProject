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
    public bool follow = false;


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
            }
            if (follow)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.position = mTarget.transform.position;
                }

            
                Vector3 scale = new Vector3((direction.x / Mathf.Abs(direction.x)), 1, 0);
                transform.localScale = scale;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

}
