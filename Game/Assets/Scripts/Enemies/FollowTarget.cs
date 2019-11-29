using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    
    Transform mTarget;

    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    private void Start()
    {
        mTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
        if(mTarget != null)
        {
            Vector2 direction = mTarget.transform.position - transform.position;
            if(direction.magnitude <= mFollowRange)
            {
                if(direction.magnitude > mArriveThreshold)
                {
                    transform.Translate (direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.position = mTarget.transform.position;
                }
            }
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }
}
