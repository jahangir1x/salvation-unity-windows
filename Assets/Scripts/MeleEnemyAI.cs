using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyAI : MonoBehaviour
{
    [HideInInspector]
    
    public bool notPatrolling;
    public Rigidbody2D rb;
    public Transform groundChecker;
    public Transform bulletPossition;
    Transform player;
    public Transform playerChecker;

    public LayerMask environmentMask;
    public LayerMask playerMask;
    private bool turn, canShoot;
    public bool isActivated;
    public GameObject sword;
    public EnemyHealth enemyHealthAccess;

    Animator animatorEnemyGround;
    float walkSpeed = 100f, range = 10f, playerDistance;
    float timeBetweenShoots = 1f;
    float foodRadius = .4f;
    public bool turn1;

    // Start is called before the first frame update
    void Start()
    {
        animatorEnemyGround = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notPatrolling = true;
        canShoot = true;
        enemyHealthAccess = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // for enemy damage effect 
        if (enemyHealthAccess.damageEffect)
        {
            notPatrolling = false;
        }


        if (notPatrolling)
        {
            turn = !Physics2D.OverlapCircle(groundChecker.position, foodRadius, environmentMask);
            turn1 = Physics2D.OverlapCircle(playerChecker.transform.position, foodRadius, environmentMask);
            Patrol();
        }

        playerDistance = Vector2.Distance(transform.position, player.position);
        // if player in range and player is on or upside platform to detect
        if (playerDistance <= range && player.position.y + 2f >= transform.position.y)
        {

            //check if player is fornt or back to flip for shooting
            if (player.position.x > transform.position.x && transform.localScale.x > 0 || player.position.x < transform.position.x && transform.localScale.x < 0)
            {
                Flip();
            }
            //for not farther movement making it false
            if (Physics2D.OverlapCircle(playerChecker.transform.position, foodRadius, playerMask))
                notPatrolling = false;
            else
                notPatrolling = true;
            if (canShoot && (rb.velocity.x < 30f || rb.velocity.x > -50f))
            {
                StartCoroutine(Shoot());                                  // calling for shoot in every timelap ends
            }
        }
        else
        {
            notPatrolling = true;                                         //to make able to farther patroll
        }
        // if died stop movement
        if (enemyHealthAccess.enemyHealth <= 0)
        {
            rb.velocity = Vector2.zero;
        }


        if (playerDistance > 1.5f)
        {
            animatorEnemyGround.Play("Walk");
        }

    }
    // make player patroll
    void Patrol()
    {
        //if there are no platform farther or any obstacle;
        if (turn || turn1)
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
    }


    IEnumerator Shoot()
    {
        //setting it false to prevent shooting continously 
        canShoot = false;
        
        yield return new WaitForSeconds(timeBetweenShoots);
        animatorEnemyGround.Play("Attack");

        sword.SetActive(true);
        int dir;
        if (transform.position.x > player.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        //canShoot = true;
        yield return new WaitForSeconds(.3f);
        SwordOff();
        canShoot = true;


    }

    void SwordOff()
    {
        sword.SetActive(false);
    }
}
