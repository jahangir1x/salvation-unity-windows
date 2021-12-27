using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainoAI : MonoBehaviour
{ 
    [SerializeField]
    public Transform checker;
    public LayerMask environmentMask;
    public LayerMask playerMask;
    public bool attacking;
    public float pauseTime = 1f;
    private Rigidbody2D rb;
    public bool turn;
    public float foodRadius = .4f;
    public float attackSpeed = 1000f;
    public float walkSpeed = 100f;
    public float viewRange = 10f;
    public bool normalMode;

    bool insideCoroutine;

    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        normalMode = true;
        attacking = false;
        turn = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //while in normal mode and patrolling 
        if (normalMode)
        {
            turn = Physics2D.OverlapCircle(checker.position, foodRadius, environmentMask);
            Patrol();
            attacking = false;
            //StopCoroutine(Attack());
        }
        else
        {
            if (!insideCoroutine)
            {
                StartCoroutine(Attack());
                insideCoroutine = true;
            }
        }

        if((rb.velocity.x > 500 || rb.velocity.x < 500)  && (Physics2D.OverlapCircle(checker.position, foodRadius, environmentMask) || Physics2D.OverlapCircle(checker.position, foodRadius, playerMask)))
        {
            rb.velocity = Vector2.zero;
            attacking = false;
            StartCoroutine(Again());
            //Again();
        }
        
        if(!isRunning)
            if (canSeePlayer(viewRange))
            {
                rb.velocity = Vector2.zero;
                normalMode = false;
                isRunning = true;
            }

        if (attacking)
        {
            rb.velocity = new Vector2(attackSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }

    }
   IEnumerator Again()
    {
        yield return new WaitForSeconds(2f);
        insideCoroutine = false;
        normalMode = true;
        isRunning = false;
    }
    // make player patroll
    void Patrol()
    {
        //if there are no platform farther or any obstacle;
        if (turn)
        {
            Flip();
        }
        // change velocity direction
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    // to flip enemy
    void Flip()
    {
        Vector3 Scale = transform.localScale;

        Scale.x *= -1;

        transform.localScale = Scale;

        walkSpeed *= -1;
        attackSpeed *= -1;
    }



    //Attacking mode code goes here...........
    private bool canSeePlayer(float distance)
    {
        bool val = false;
        float castDistance;
        if (transform.localScale.x > 0)
            castDistance = distance;
        else
            castDistance = -distance;



        Vector2 endposition = checker.position + Vector3.right * castDistance;

        RaycastHit2D hit = Physics2D.Linecast(checker.position, endposition, 1 << LayerMask.NameToLayer("Player"));

        

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(checker.position, hit.point, Color.red);
        }
        else
        {
            //Debug.DrawLine(checker.position, endposition, Color.red);
        }
        return val;
    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(pauseTime);
        attacking = true;
    }
}
