using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlip = false;

    public float speed = 2.5f,time=1f;
    public float attackRange = 0.5f;
    Rigidbody2D rb;
    Animator anim;
    public GameObject clone;
    public Vector3 Offset;
    BossHealth bossHealth;
    Shoot shoot;

    public int dir=1;

    public float spawnDelay;

    public BossCutsceneManager bossCutsceneManager;


    bool isAttacking = false;

    public GameObject spawnPoint;
    public enum BossStage
    {
        None,
        FirstStage,
        SecondStage,
        ThirdStage,
        FourthStage,
        DeathStage

    }
     public BossStage bossStage;

    public GameObject lastStone;

    bool isInsideCoroutine = false;

    public int isover = 0;
    public int stoneGenerated = 0;

    public float stoneForce;

    public bool stoneCollected = false;

    public bool killBoss = false;
    public void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        bossHealth = GetComponent<BossHealth>();

        shoot = gameObject.GetComponent<Shoot>();

        bossStage = BossStage.FirstStage;
    }
    public void LookAtPlayer()
    {
        Vector3 flipp = transform.localScale;
        flipp.z *= -1f;

        if(rb.position.x>player.position.x&&isFlip)
        {
            transform.localScale = flipp;
            transform.Rotate(0f, 180f, 0f);
            isFlip = false;
            dir = 1;
        }
        else if(rb.position.x<player.position.x && !isFlip)
        {
            transform.localScale = flipp;
            transform.Rotate(0f, 180f, 0f);
            isFlip = true;

            dir = -1;

        }
    }

    GameObject stone;
    private void Update()
    {

        if (bossStage == BossStage.None)
        {
            anim.Play("idle");
            if (isover == 1)
            {
                shoot.enabled = false;

                gameObject.GetComponent<Collider2D>().enabled = false;

                // spawn last stone. 
                if (stoneGenerated == 0 && transform.childCount < 5)
                {
                     stone = Instantiate(lastStone, spawnPoint.transform.position, Quaternion.identity);
                     stoneGenerated = 11;

                }

                if (stoneCollected)
                    bossStage = BossStage.DeathStage;


            }
            else
            {
                shoot.enabled = false;
                //anim.Play("idle");
            }

        }
        if (bossStage == BossStage.FirstStage)
        {
            LookAtPlayer();
            Vector2 newPos = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);

            if (Vector2.Distance(player.position, rb.position) >= 0.5f)
            {
                if (!isAttacking)
                    rb.MovePosition(newPos);
            }

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                anim.Play("attack");
                isAttacking = true;
            }
            else
            {
                anim.Play("runAnim");
                isAttacking = false;

            }
        }
        if(bossStage==BossStage.SecondStage)
        {
            if (time>0)
                anim.Play("idle");

            if(time<=0f)
            {
                if (!isInsideCoroutine)
                {
                    
                    StartCoroutine(Clone());

                }
               
            }

        }
        if(bossStage== BossStage.ThirdStage)
        {
            anim.Play("idle");

            LookAtPlayer();
            shoot.enabled = true;
           // shoot.speed = 100f;
        }
        if (bossStage == BossStage.FourthStage)
        {
            if (time > 0f)
            {
                anim.Play("idle");
                
            }
            if (time <= 0f)
            {
                if (!isInsideCoroutine)
                    StartCoroutine(Clone());

            }

            if (shoot.enabled == false)
            {
                shoot.enabled = true;
            }
           // shoot.speed = 200f;
        }

        if (bossStage == BossStage.DeathStage)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            //play cutscene...
            if (transform.position.x > player.transform.position.x)
            {
                if (player.transform.localScale.x < 0)
                    player.GetComponent<NormalPlayerMovement>().FlipCharacterDirection();
            }
            else
            {
                if (player.transform.localScale.x > 0)
                    player.GetComponent<NormalPlayerMovement>().FlipCharacterDirection();
            }
            bossCutsceneManager.PlayFinalCutscene();
        }
        time -= Time.deltaTime;
    }
     IEnumerator Clone()
    {
        anim.Play("skeleton_appear");

        isInsideCoroutine = true;

       yield return new WaitForSeconds(spawnDelay);
       
        GameObject c1= Instantiate(clone, spawnPoint.transform.position, Quaternion.identity,transform);

        //c1.tag = "Skeleton";
        time = 1f;

        isInsideCoroutine = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpecialBullet"))
        {
            anim.Play("dead");
        }
    }



}
