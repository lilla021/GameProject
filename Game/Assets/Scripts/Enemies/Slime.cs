using UnityEngine;
using System.Collections;

public class Slime : Enemy {

    [SerializeField]
    float mFollowSpeed;
    float currentSpeed;
    [SerializeField]
    float mFollowRange;
    Vector2 direction;

    float attackRange = 1.4f;

    private void Start() {
        player = FindObjectOfType<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        HP = 10;
        attack = 5;
        xp = 1;
    }

    void Update() {
        isGrounded = checkGrounded();
        if (PlayerData.IsInDream) dropChance = 10;
        if (isGrounded) {
            Move();
            Attack();
        }
        Death();
    }

    protected override void Move() {
        currentSpeed = 0;
        direction = player.transform.position - transform.position;
        transform.localScale = new Vector2(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        if (direction.magnitude <= mFollowRange && direction.magnitude > attackRange) {
            currentSpeed = Mathf.Sign(transform.localScale.x) * mFollowSpeed;
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime, Space.World);
        }
        mAnimator.SetFloat("Speed", currentSpeed);
    }

    protected override void Death() {
        if (HP <= 0) {
            isDead = true;
            onDeath();
            Destroy(gameObject);
        }
    }

    protected override void Attack() {
        if (direction.magnitude <= attackRange) {
            mAnimator.Play("Attack");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            player.getHit(attack);
        }
    }
}
