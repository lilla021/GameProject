using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : Enemy
{
    bool ShieldOn = false;
    bool ShieldBash = false;
    Vector2 direction;

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

    float mArriveThreshold = 0.05f;
    bool follow;
    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        HP = 100;
        attack = 10;
        xp = 10;
        weight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        IsInDream();
        if (!isDead) {
            Move();
            Attack();
        }
        Death();
    }

    protected override void Move() {
        direction = player.transform.position - transform.position;

        follow = direction.magnitude <= mFollowRange;
        mAnimator.SetBool("isRunning", follow);

        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            if (!PlayerData.IsInDream && follow) {
                if (direction.magnitude > mArriveThreshold) {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime);
                } else {
                    transform.position = player.transform.position;
                }
            }
            transform.localScale = new Vector3(Mathf.Sign(direction.x), transform.localScale.y, 0);
        }
    }

    protected override void Attack() {
        isAttack = direction.magnitude <= mAttackRange;
        mAnimator.SetBool("isAttack", isAttack);
    }

    protected override void Death()
    {
        if(HP <= 0f)
        {
            isDead = true;
            mAnimator.SetBool("isAttack", false);
            mAnimator.Play("Death");
        }
    }

    void IsInDream()
    {
        if(PlayerData.IsInDream && !isDead)
        {
            defense = 15f;
            ShieldOn = true;
            mAnimator.SetBool("isShield", ShieldOn);
            ShieldBash = true;
            mAnimator.SetBool("isRunning", false);
            mAnimator.SetBool("isAttack", false);
        }
        else
        {
            defense = 0f;
            ShieldBash = false;
            ShieldOn = false;
            mAnimator.SetBool("isShield", ShieldOn);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            player.getHit(attack);
        }
    }
}
