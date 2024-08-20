using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rigidbody;
    public float speed = 20.0f;

    public float rangeAttack = 4;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // tìm vị trí player để đi theo
        rigidbody = animator.GetComponent<Rigidbody2D>();
        //boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss_Flip>().FindPlayer(); // gọi scrips để quay mặt về phía player
        Vector2 target = new Vector2(player.position.x, rigidbody.position.y); // tìm vị trí mà muốn đi tới 
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, speed * Time.deltaTime); // di chuyển boss từ vị trí hiện tại đến vị trí tìm được 
        rigidbody.MovePosition(newPos);

        if (Vector2.Distance(player.position, rigidbody.position) <= rangeAttack)
        {
            animator.SetTrigger("attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void onstatemove(animator animator, animatorstateinfo stateinfo, int layerindex)
    //{
    //    // implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
