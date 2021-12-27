using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed=2.5f;
    public float attackRange = 0.5f;
    Transform Player;
    Rigidbody2D rb;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb=animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(Player.position.x, 0f);

       Vector2 newPos= Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        newPos = new Vector2(newPos.x, 0);

        if (Vector2.Distance(Player.position, rb.position) >=0.5f)
        {
            Debug.Log(Vector2.Distance(Player.position, rb.position));
            rb.MovePosition(newPos);
        }
       if( Vector2.Distance(Player.position, rb.position) <=attackRange)
        {
            animator.Play("attack");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }

   
}
