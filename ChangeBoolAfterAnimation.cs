using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoolAfterAnimation : StateMachineBehaviour
{
    public string animationToTurnOff;
    private int animationTime;
    TimeSpan timeUntilFinished;
    DateTime timeWhenFinished = TimeManagement.CurrentTime();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeUntilFinished = new TimeSpan(0, 0, 0, 0, (int)(stateInfo.length * 1000));
        timeWhenFinished = TimeManagement.CurrentTime() + timeUntilFinished;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeWhenFinished < TimeManagement.CurrentTime())
        {
            animator.SetBool(animationToTurnOff, false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
