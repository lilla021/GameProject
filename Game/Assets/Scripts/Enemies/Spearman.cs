using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Enemy
{
    [SerializeField]
    float speed;
    float currentSpeed;

    [SerializeField]
    float aggroRange;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackTime;
    float attackCounter;

    [SerializeField]
    float dashRange;
    [SerializeField]
    float dashForce;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        HP = 75;
        attack = 15;
        xp = 5;
        attackCounter = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset the constraints after using a dash.
        if (mRigidbody.constraints == (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation) && (Mathf.Abs(mRigidbody.velocity.x) <= speed * Time.fixedDeltaTime + 1)) {
            mRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

        if (!isDead) {
            Move();
            Attack();
        }
        Death();
    }

    protected override void Move() {
        currentSpeed = 0;
        direction = player.transform.position - transform.position;
        transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if (direction.magnitude <= aggroRange && direction.magnitude > attackRange) {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + (Vector3.right * Mathf.Sign(transform.localScale.x) * 0.5f), direction.normalized, aggroRange);
            if (rayHit.collider) {
                if (rayHit.collider.CompareTag("Player")) {
                    currentSpeed = speed * Mathf.Sign(transform.localScale.x);
                    transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
                }
            }
        } else if (direction.magnitude <= attackRange) {
            mRigidbody.velocity = new Vector2(0, mRigidbody.velocity.y);
        }
        mAnimator.SetFloat("Speed", Mathf.Abs(currentSpeed));
    }
    
    protected override void Attack() {
        if (attackCounter >= attackTime) {
            if (!PlayerData.IsInDream) {
                if (direction.magnitude <= dashRange && Mathf.Abs(transform.position.y - player.transform.position.y) <= 1) {
                    attack = 15;
                    mAnimator.Play("Attack1");
                    attackCounter = 0;
                }
            } else {
                if (direction.magnitude <= attackRange) {
                    attack = 10;
                    mAnimator.Play("Attack2");
                    attackCounter = 0;
                }
            }
        }
        attackCounter += Time.deltaTime;
    }

    protected override void Death() {
        if (HP <= 0) {
            mAnimator.Play("Death");
        }
    }

    void Dash() {
        if (direction.magnitude > attackRange) {
            mRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            mRigidbody.AddForce(Vector2.right * transform.localScale.x * dashForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            player.getHit(attack);
        }
    }
}
