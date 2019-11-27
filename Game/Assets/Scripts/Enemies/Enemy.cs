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
    protected float xp = 0;

    protected bool isDead = false;
    protected bool isGrounded = false;

    [SerializeField]
    GameObject hpPotion;
    [SerializeField]
    GameObject manaPotion;

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

    private void OnDestroy() {
        PlayerData.CurrentXP += xp;
        int potionHP = Random.Range(0, 6);
        int potionMP = Random.Range(0, 6);
        if (potionHP == 1) Instantiate(hpPotion, transform.position - Vector3.right*0.02f, Quaternion.identity);
        if (potionMP == 1) Instantiate(manaPotion, transform.position + Vector3.right*0.02f, Quaternion.identity);
    }
}