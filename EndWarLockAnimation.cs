using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarLockAnimation : StateMachineBehaviour
{
    public GameObject ghost;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("special", false);
        Vector3 spawnPoint = animator.gameObject.transform.position;

        //EmptyBackgroundScript.RayCastToGround(animator.gameObject.transform.position)

        
        if (animator.CompareTag("team1"))
        {
            GameObject spawnCreature = Instantiate(ghost, new Vector3(spawnPoint.x - 0.2f, spawnPoint.y + 1, spawnPoint.z), Quaternion.identity);
        }
        else
        {
            GameObject spawnCreature = Instantiate(ghost, new Vector3(spawnPoint.x + 0.2f, spawnPoint.y + 1, spawnPoint.z), Quaternion.identity);
            spawnCreature.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
            spawnCreature.GetComponent<SpriteRenderer>().flipX = !spawnCreature.GetComponent<SpriteRenderer>().flipX;
        }
        //animator.SetBool("moving", true);
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
