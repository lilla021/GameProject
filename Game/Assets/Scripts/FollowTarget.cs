using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    [SerializeField]
    float jumpXMultiplier;
    [SerializeField]
    float jumpYMultiplier;

    public bool facingRight = true;
    public bool follow = false;

    GroundCheck[] groundCheck;
    bool isGrounded = false;

    Animator animator;
    Rigidbody2D rb;

    float mArriveThreshold = 0.05f;
    void Start()
    {
        groundCheck = transform.GetComponentsInChildren<GroundCheck>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);

        if (mTarget != null)
        {
            // TODO: Make the enemy follow the target "mTarget"
            //       only if the target is close enough (distance smaller than "mFollowRange")
            
            Vector2 direction = mTarget.transform.position - transform.position;
            
            if (direction.magnitude <= mFollowRange)
            {
                follow = true;              
            }
            if (follow && !PlayerData.IsInDream)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    transform.Translate(Vector2.right * (direction.x / Mathf.Abs(direction.x)) * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.position = mTarget.transform.position;
                }
                
                Vector3 scale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
                transform.localScale = scale;
            } else if (follow) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                    animator.Play("PrepareJump");
                } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                    rb.AddForce(new Vector2((direction.x / Mathf.Abs(direction.x)) * jumpXMultiplier, jumpYMultiplier), ForceMode2D.Impulse);
                }

                Vector3 scale = new Vector3((direction.x / Mathf.Abs(direction.x)), transform.localScale.y, 0);
                transform.localScale = scale;
            }
        }
    }

    bool checkGrounded() {
        foreach (GroundCheck g in groundCheck) {
            if (g.CheckGrounded(0.35f, LayerMask.GetMask("Ground"), gameObject)) {
                return true;
            }
        }
        return false;
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

}
