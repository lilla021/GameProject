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
    protected float Mdefense = 0;
    protected float attack;
    protected float xp = 0;
    protected int dropChance = 5; //Enemies will have 1/dropChance chance to drop each potions
    public float weight { get; protected set; } = 0;

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
    public void GetMagicHit(float damage)
    {
        HP -= damage - Mdefense;
    }
    protected bool checkGrounded() {
        foreach (GroundCheck g in groundCheck) {
            if (g.CheckGrounded(0.35f, LayerMask.GetMask("Ground"), gameObject)) {
                return true;
            }
        }
        return false;
    }

    protected void onDeath() {
        PlayerData.CurrentXP += xp;
        if (!PlayerData.IsInDream)
        {
            int potionHP = Random.Range(0, dropChance);
            int potionMP = Random.Range(0, dropChance);
            if (potionHP == 1) Instantiate(hpPotion, transform.position - Vector3.right * 0.02f + Vector3.up * 0.2f, Quaternion.identity);
            if (potionMP == 1) Instantiate(manaPotion, transform.position + Vector3.right * 0.02f + Vector3.up * 0.2f, Quaternion.identity);
        }
    }
}