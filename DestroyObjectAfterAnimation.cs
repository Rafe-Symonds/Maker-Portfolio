using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterAnimation : StateMachineBehaviour
{
    bool alreadyCalledOnStateEnter = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(alreadyCalledOnStateEnter == false)
        {
            alreadyCalledOnStateEnter = true;
            animator.SetBool("idle", false);
            


            //Lines below move the collider when a creature dies to have it stop colliding and call triggers exit 
            GameObject contactCollider = animator.gameObject.transform.Find("ContactCollider").gameObject;
            contactCollider.transform.position = new Vector3(-100, 0, 0);
            GameObject groundCollider = animator.gameObject.transform.Find("GroundCollider").gameObject;
            groundCollider.transform.position = new Vector3(-100, 0, 0);
            
            Rigidbody2D rigidbody2D = animator.gameObject.GetComponent<Rigidbody2D>();
            if(rigidbody2D != null)
            {
                //Debug.Log("Turning off gravity");
                rigidbody2D.gravityScale = 0;
            }
            
            GeneralMovementNew generalMovementNew = animator.gameObject.GetComponentInChildren<GeneralMovementNew>();
            if (generalMovementNew != null)
            {
                generalMovementNew.moving = false;
            }
            Destroy(generalMovementNew);
            //Destroy(contactCollider);
            //Debug.Log("Moving contact collider");


            Transform deathCollider = animator.gameObject.transform.Find("DeathCollider");
            if (deathCollider != null)
            {
                deathCollider.GetComponent<TriggersAndCollidersForDeathDamage>().AttackOnDeath();
            }




            if (generalMovementNew != null)
            {
                int randomNumber = UnityEngine.Random.Range(0, 100);
                if (randomNumber > 90 && animator.gameObject.tag.Equals("team2"))
                {
                    Vector3 deathPoint = new Vector3(animator.gameObject.transform.position.x, 
                        animator.gameObject.transform.position.y + 1, 0);
                    GameObject treasureChest = Instantiate(Resources.Load("Prefabs/General/TreasureChest") as GameObject,
                        deathPoint, Quaternion.identity);
                    TreasureChestScript treasureChestScript = treasureChest.GetComponentInChildren<TreasureChestScript>();
                    //Debug.Log("================ " +treasureChestScript);
                    treasureChestScript.parent = animator.gameObject.transform.Find("ContactCollider").gameObject;
                }
            }
            //using math.max so that the creatures unblock correctly
            Destroy(animator.gameObject, Mathf.Max(stateInfo.length, 0.1f));
        }
        //Debug.Log("Destroy Creature");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
