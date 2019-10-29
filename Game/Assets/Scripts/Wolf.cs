using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    Animator mAnimator;
    private bool dirRight = true;
    public float speed = 2.0f;
    bool mWalk;

    Vector2 mFacingDirection;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateAnimator();
    }

    void Move()
    {
        mWalk = true;
        if (dirRight)
        {
            FaceDirection(-Vector2.right);
        }
        else
        {
            FaceDirection(Vector2.right);
        }

    }
    private void UpdateAnimator()
    {
        mAnimator.SetBool("isWalking", mWalk);
     
    }

    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }

    private void FaceDirection(Vector2 direction)
    {
        mFacingDirection = direction;
        if (direction == Vector2.right)
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }
}
