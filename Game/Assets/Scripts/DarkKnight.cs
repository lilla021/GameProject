using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : MonoBehaviour
{

    Rigidbody2D mRigidbody;
    Animator mAnimator;
    public static float KnightHP = 100f;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (DarkKnightFollow.follow == true)
        {
            mAnimator.SetBool("isRunning", true);
        }
        else
        {
            mAnimator.SetBool("isRunning", false);
        }
        Death();
    }

    void Death()
    {
        if(KnightHP == 0f)
        {
            mAnimator.Play("Death");
        }
    }
}
