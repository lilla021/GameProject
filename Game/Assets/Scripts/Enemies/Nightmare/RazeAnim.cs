using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazeAnim : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
