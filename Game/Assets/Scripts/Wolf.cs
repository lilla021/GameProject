using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    Rigidbody2D mRigidbody;
    Animator mAnimator;
    FollowTarget followTarget;
    public float speed = 2.0f;
    bool mWalk;
    bool mRun;

    public static float WolfHP = 60f;

    public float min = 2f;
    public float max = 3f;
    public float CurPos;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        followTarget = GetComponent<FollowTarget>();
        min = transform.position.x;
        max = transform.position.x + 5f;
        CurPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget.follow == true)
        {
            mRun = true;
        }
        else
        {
            Move();
        }

        Death();

        UpdateAnimator();
    }

    void Move()
    {
        mWalk = true;

        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, max - min) + min, transform.position.y, transform.position.z);

        if (transform.position.x > CurPos)
        {
            CurPos = transform.position.x;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        if (transform.position.x < CurPos)
        {
            CurPos = transform.position.x;
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", mWalk);
        mAnimator.SetBool("isRunning", mRun);
    }

    void Death()
    {
        if(WolfHP == 0f)
        {
            Destroy(gameObject);
        }
    }

}
