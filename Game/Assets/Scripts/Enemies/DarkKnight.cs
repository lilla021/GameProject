using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : Enemy
{
    public bool ShieldOn = false;
    public bool ShieldBash = false;
    public float KnightAtk = 10f;
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
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        HP = 100;
        attack = 10;
    }

    // Update is called once per frame
    void Update()
    {
        IsInDream();
        UpdateAnimator();
        Move();
        Attack();
        Death();
    }

    protected override void Move() {
        direction = player.transform.position - transform.position;

        follow = direction.magnitude <= mFollowRange;

        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            if (!PlayerData.IsInDream && follow) {
                if (direction.magnitude > mArriveThreshold) {
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime);
                } else {
                    transform.position = player.transform.position;
                }
            }
            transform.localScale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
        }
    }

    protected override void Attack() {
        isAttack = direction.magnitude <= mAttackRange;
    }

    protected override void Death()
    {
        if(HP <= 0f)
        {
            mAnimator.SetBool("isAttack", false);
            mAnimator.Play("Death");
        }
    }

    void IsInDream()
    {
        if(PlayerData.IsInDream == true)
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

    void UpdateAnimator() {
        mAnimator.SetBool("isRunning", follow);
        mAnimator.SetBool("isAttack", isAttack);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerController player = (PlayerController)collision.GetComponent(typeof(PlayerController));
            player.getHit(attack);
        }
    }
}
