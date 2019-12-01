﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    Vector2 direction;
    public GameObject me;

    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    float mAttackRange;

    float mArriveThreshold = 0.05f;
    bool follow;
    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        me = GameObject.Find("Player");
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        HP = 100;
        attack = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        if (isGrounded) {
            Move();
            Attack();
            IsInDream();
        }
        Death();
    }

    protected override void Move()
    {
        direction = me.transform.position - transform.position;
        follow = direction.magnitude <= mFollowRange;
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (!PlayerData.IsInDream && follow)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = player.transform.position;
                }
            }
            transform.localScale = new Vector3(-Mathf.Sign(direction.x), transform.localScale.y, 0);
        }
    }

    protected override void Attack()
    {       
        isAttack = direction.magnitude <= mAttackRange;
    }

   
    protected override void Death()
    {
        if (HP <= 0f)
        {
            mAnimator.SetBool("isAttack", false);
            Sorcerer.GhoulSummon = false;
            mAnimator.Play("Death");
        }
    }

    void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", follow);
        mAnimator.SetBool("isAttack", isAttack);
        isGrounded = checkGrounded();
    }

    void IsInDream()
    {
        if (PlayerData.IsInDream == true)
        {
            Sorcerer.GhoulSummon = false;
            mAnimator.Play("Death");
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            attack += 0.2f;
            Debug.Log(attack);
            PlayerController player = (PlayerController)collision.GetComponent(typeof(PlayerController));
            player.getHit(attack);
        }
    }
}
