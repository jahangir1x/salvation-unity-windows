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
    BossReplica bossReplica;
    Shoot shoot;

    public int dir=1;


    bool isAttacking = false;
    public enum BossStage
    {
        None,
        FirstStage,
        SecondStage,
        ThirdStage,
        FourthStage,
        FifthStage,
        DeathStage

    }
     public BossStage bossStage;

    bool isInsideCoroutine = false;
    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        bossHealth = GetComponent<BossHealth>();

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

    private void Update()
    {
        if (bossStage == BossStage.None)
        {
            anim.Play("idle");
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
            if(time>0)
                anim.Play("idle");

            if(time<=0f)
            {
                if (!isInsideCoroutine)
                {
                    anim.Play("skeleton_appear");

                    StartCoroutine(Clone());

                }
               
            }

        }
        if(bossStage == Boss.BossStage.ThirdStage)
        {
            bossReplica = gameObject.GetComponent<BossReplica>();
            bossReplica.enabled = true;
        }
        if(bossStage==Boss.BossStage.FourthStage)
        {
            LookAtPlayer();
            bossReplica.enabled = false;
            shoot = gameObject.GetComponent<Shoot>();
            shoot.enabled = true;
           // shoot.speed = 100f;
        }
        if (bossStage == Boss.BossStage.FifthStage)
        {
            if (time > 0f)
            {
                LookAtPlayer();
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
        time -= Time.deltaTime;
    }
     IEnumerator Clone()
    {
        isInsideCoroutine = true;

       yield return new WaitForSeconds(0.6f);
       
        GameObject c1= Instantiate(clone, transform.position + Offset, Quaternion.identity);

        //c1.tag = "Skeleton";
        time = 1f;

        isInsideCoroutine = false;
        //GameObject c2 = Instantiate(clone, transform.position + Offset, Quaternion.identity);
        //  yield return new WaitForSeconds(2f);
        //GameObject c3 = Instantiate(clone, transform.position + Offset, Quaternion.identity);
    }
}
