using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// flying enemy player detection and transformations
public class EnemyFace : MonoBehaviour
{
    public Animator animateEnemyGround;         // player animation
    public AIPath aiPath;                       //AiPath script
    Transform player;                    // player transform
    AIDestinationSetter flyingEnemyAi;          //aidestination setter script
    public GameObject bulletFlying;             //bullet object
    public Transform bulletPossition;           //bullet starting position
    public EnemyHealth enemyHealthAccess;              //enemy health acces                          
    float timeBetweenShoots = 1f;                  // delay time between fire
    public bool canShoot;                       //check if ready to shoot or in enough closer to shoot
    float distance;                             //distance between player and enemy 
    float playerDetectionDistance = 10f;        // lowest distance to track player
    bool isfreezed;                               // true if freezed ability active
    int direction = 1;                          //Check direction to flip
    Vector3 originalScale;                      // to store original scale or enemy 
    float freezTime = 5f;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = transform.localScale;
        canShoot = true;
        enemyHealthAccess = GetComponent<EnemyHealth>();
        flyingEnemyAi = GetComponent<AIDestinationSetter>();
    }
    // Update is called once per frame
    void Update()
    {

        isfreezed = enemyHealthAccess.freezed;    // setting freezed by using Enemyhealth access
        // when enemy is freezed
        if (isfreezed)
        {
            timeBetweenShoots = 6f;
            animateEnemyGround.SetBool("GetFreez", true); // freezed animation
            StartCoroutine(MakeMovable());              //to make moveable after certain time;
        }
        //check if need to flip;
        if(aiPath.desiredVelocity.x * direction < 0f )
            Flip();
        // check if ready to shoot
        if(aiPath.desiredVelocity.x == 0 && aiPath.desiredVelocity.y == 0 && distance <= 10f){
            // if canShoot cause it can't shoot in freezed time
             if(canShoot){
                //to shoot
                StartCoroutine(Shoot());
             }
        }

        //Take enemy distance and determine whene to move;
         distance = Vector2.Distance( transform.position , player.position);
        // to move  enemy enable enemy Ai
         if(distance <= playerDetectionDistance && !isfreezed){
            flyingEnemyAi.enabled = true;
         }else{
            flyingEnemyAi.enabled = false;
         }


    }
    // to flip enemy
    void Flip()
    {
        direction *= -1;

        Vector3 Scale = transform.localScale;

        Scale.x = originalScale.x * direction;

        transform.localScale = Scale;
    }

    // to shoot
    IEnumerator Shoot(){
        //setting it false to prevent shooting continously 
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShoots);

        GameObject firedBullet = Instantiate(bulletFlying, bulletPossition.position, Quaternion.identity);
        firedBullet.transform.position = bulletPossition.position;

        firedBullet.transform.eulerAngles = new Vector3(firedBullet.transform.eulerAngles.x, firedBullet.transform.eulerAngles.y, firedBullet.transform.eulerAngles.z + RotaitonofZ(bulletPossition.position));

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

    float RotaitonofZ(Vector2 position)
    {
        float x, y;
        x = player.transform.position.x - position.x;
        y = player.transform.position.y - position.y;

        float rotation = Mathf.Atan(y/x);

        rotation *= 180/Mathf.PI;

        return rotation;
    }

    // to make moveable after freezed tiem ends
    IEnumerator MakeMovable()
    {
        yield return new WaitForSeconds(freezTime);
        isfreezed = false;                                     // make freezed false
        animateEnemyGround.SetBool("GetFreez", false);       // change animation to normal from freezed
        timeBetweenShoots = 1f;
    }
}
