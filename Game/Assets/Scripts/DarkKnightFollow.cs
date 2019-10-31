using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnightFollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    float mAttackRange;
    [SerializeField]
    float dashForce;
    [SerializeField]
    float dashRange;

    public bool facingRight = true;
    public static bool follow;
    bool isAttack = false;


    Animator mAnimator;

    DarkKnight dark;

    float mArriveThreshold = 0.05f;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        dark = GetComponent<DarkKnight>();
    }

    void Update()
    {


        if (mTarget != null)
        {
            // TODO: Make the enemy follow the target "mTarget"
            //       only if the target is close enough (distance smaller than "mFollowRange")

            Vector2 direction = mTarget.transform.position - transform.position;
            if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                if (PlayerData.IsInDream == false)
                {
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
                        }
                        if (direction.magnitude <= mAttackRange)
                        {
                            isAttack = true;
                            mAnimator.SetBool("isAttack", isAttack);
                            if (direction.magnitude > mArriveThreshold)
                            {
                                transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                            }
                            else
                            {
                                transform.position = mTarget.transform.position;
                            }
                        }
                    }
                    else
                    {
                        follow = false;
                        isAttack = false;
                        mAnimator.SetBool("isAttack", isAttack);
                    }
                }
                else
                {
                    follow = false;
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
