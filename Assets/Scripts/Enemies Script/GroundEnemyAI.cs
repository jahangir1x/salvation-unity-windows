using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : MonoBehaviour {

    public bool notPatrolling;
    [HideInInspector]
    public Rigidbody2D rb;
    public Transform groundChecker , platformChecker;
    public Transform bulletPossition;
    Transform player;

    public LayerMask environmentMask;
    private bool turn , canShoot;
    public bool isActivated;
   // public Collider2D bodyCollider;
    public Animator animatorEnemyGround;
    public GameObject bullet;
    public EnemyHealth enemyHealthAccess;


    float walkSpeed = 100f , range = 10f , playerDistance ;
    float timeBetweenShoots = 1.15f;
    float foodRadius = .4f;
    bool turn1;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notPatrolling = true;
        canShoot = true;
        enemyHealthAccess = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update(){
        turn = !Physics2D.OverlapCircle(groundChecker.position, foodRadius, environmentMask);
        turn1 = Physics2D.OverlapCircle(platformChecker.position, foodRadius, environmentMask);

        if (notPatrolling){
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
            notPatrolling = false;
            //walkSpeed = 0f;
            animatorEnemyGround.SetBool("Shooting", true);      // to start shooting animation
            if (canShoot)
            {
                StartCoroutine(Shoot());                                  // calling for shoot in every timelap ends
            }
        }
        else
        {
            notPatrolling = true;                                         //to make able to farther patroll
            animatorEnemyGround.SetBool("Shooting", false);     // to stop shooting animation and back to default animation
        }

    }
    // make player patroll
    void Patrol(){
        //if there are no platform farther or any obstacle;
        if(turn || turn1){
            Flip();
        }
        // change velocity direction
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime , rb.velocity.y);
    }
    // to flip enemy
    void Flip(){
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

        GameObject firedBullet = Instantiate(bullet, bulletPossition.position, Quaternion.identity);
        firedBullet.transform.position = bulletPossition.position;
        firedBullet.transform.rotation = transform.rotation;

        int dir;
        if (transform.position.x > player.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        Vector2 bulletScale = firedBullet.transform.localScale;
        bulletScale.x *= dir;
        firedBullet.transform.localScale = bulletScale;
        canShoot = true;

    }

}
