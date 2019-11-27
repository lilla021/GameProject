using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected PlayerController player;

    protected GroundCheck[] groundCheck;
    protected Rigidbody2D mRigidbody;
    protected Animator mAnimator;

    protected float HP;
    protected float defense = 0;
    protected float attack;

    protected bool isDead = false;
    protected bool isGrounded = false;

    protected abstract void Move();
    protected abstract void Death();
    protected abstract void Attack();
    public void getHit(float damage) {
        HP -= damage - defense;
    }

    protected bool checkGrounded() {
        foreach (GroundCheck g in groundCheck) {
            if (g.CheckGrounded(0.35f, LayerMask.GetMask("Ground"), gameObject)) {
                return true;
            }
        }
        return false;
    }
}