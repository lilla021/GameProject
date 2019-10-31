using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : MonoBehaviour
{

    public Rigidbody2D mRigidbody;
    Animator mAnimator;
    public static float KnightHP = 100f;
    public float KnightDefense = 0f;
    public bool ShieldOn = false;
    public bool ShieldBash = false;
    public float KnightAtk = 10f;

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
        IsInDream();
        Death();
    }

    void Death()
    {
        if (KnightHP <= 0f)
        {
            mAnimator.SetBool("isAttack", false);
            mAnimator.Play("Death");
        }
    }

    void IsInDream()
    {
        if (PlayerData.IsInDream == true)
        {
            KnightDefense = 15f;
            ShieldOn = true;
            mAnimator.SetBool("isShield", ShieldOn);
            ShieldBash = true;
            mAnimator.SetBool("isRunning", false);
            mAnimator.SetBool("isAttack", false);
            mRigidbody.mass = 1000f;
        }
        else
        {
            mRigidbody.mass = 3f;
            KnightDefense = 0f;
            ShieldBash = false;
            ShieldOn = false;
            mAnimator.SetBool("isShield", ShieldOn);
        }
    }

}
