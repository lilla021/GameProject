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
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall") || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land")) {
            mRun = false;
            mWalk = false;
        }
        else if (followTarget.follow && !PlayerData.IsInDream)
        {
            mRun = true;
        }
        else if (followTarget.follow && PlayerData.IsInDream) {
            mRun = false;
            mWalk = false;
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

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run") || mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))) {
            PlayerData.CurrentHP -= 5;
        }
    }

}
