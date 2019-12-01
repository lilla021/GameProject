using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    float range;
    [SerializeField]
    float runRange;
    [SerializeField]
    float attackTime;
    float attackCounter;

    [SerializeField]
    float maxSpeed;
    bool isRunning = false;
    float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        HP = 40;
        xp = 5;
        attackCounter = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = checkGrounded();
        if (!isDead && isGrounded) {
            Move();
            Attack();
        }
        Death();
    }

    protected override void Attack() {
        attackCounter = PlayerData.IsInDream ? attackTime : attackCounter + Time.deltaTime;
        if ((player.transform.position - transform.position).magnitude <= range && !isRunning) {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + (Vector3.right * Mathf.Sign(transform.localScale.x) * 0.5f), (player.transform.position - transform.position).normalized, range);
            if (rayHit.collider && attackCounter >= attackTime) {
                if (rayHit.collider.CompareTag("Player")) {
                    mAnimator.Play("Attack");
                    attackCounter = 0;
                }
            }
        }
    }

    protected override void Death() {
        if (HP <= 0) {
            isDead = true;
            mAnimator.Play("Death");
        }
    }

    protected override void Move() {
        if (((player.transform.position + new Vector3(0.5f, 0, 0))- transform.position).magnitude <= runRange) {
            isRunning = true;
            speed = maxSpeed * Mathf.Sign((transform.position - player.transform.position).x);
        } else {
            isRunning = false;
            speed = 0;
        }
        mAnimator.SetFloat("Speed", Mathf.Abs(speed));
        if (!isRunning) {
            transform.localScale = new Vector2(Mathf.Sign((player.transform.position - transform.position).x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } else if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            transform.localScale = new Vector2(-Mathf.Sign((player.transform.position - transform.position).x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    //Create an arrow. Used in animation event.
    void createArrow() {
        Vector3 position = transform.localScale.x < 0 ? transform.position + new Vector3(0.35f, 0.1f, 0) * transform.localScale.x : transform.position + new Vector3(0.35f, -0.1f, 0) * transform.localScale.x;
        Instantiate(arrow, position, arrow.transform.rotation);
    }
}
