using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSlideBehaviour : StateMachineBehaviour
{
    Vector2 slideSizeBoxCollider = new Vector2(0.4537954f, 0.6610184f);
    Vector2 slideOffset = new Vector2(0.08034372f, -0.88f);

    Vector2 sizeOriginal;
    Vector2 offsetOriginal;

    BoxCollider2D boxCollider;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.PlayerSlide = true;

        if(boxCollider == null)
        {
            boxCollider = PlayerController.Instance.GetComponent<BoxCollider2D>();
            sizeOriginal = boxCollider.size;
            offsetOriginal = boxCollider.offset;
        }

        boxCollider.size = slideSizeBoxCollider;
        boxCollider.offset = slideOffset;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.PlayerSlide = false;
        animator.ResetTrigger("animatorSlide");
        boxCollider.size = sizeOriginal;
        boxCollider.offset = offsetOriginal;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
