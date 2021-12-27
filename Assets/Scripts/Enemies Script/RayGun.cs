using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{

    public GameObject bullet;
    public Transform checker;
    public LayerMask environmentMask;
    public LayerMask playerMask;

    LineRenderer lr;

    float viewRange = 10f;

    bool isrunning;
    bool isShooting;
    bool canShoot;

    private void Start()
    {
        canShoot = true;
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (!isrunning)
        {
            if(canSeePlayer(viewRange))
            {
                isrunning = true;
                StartCoroutine(Shooting());
            }
        }
        
        if (isShooting)
        {
            if(canShoot)
                StartCoroutine(Shoot());
            StartCoroutine(StopShooting());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(.06f);
        canShoot = true;
        Instantiate(bullet, checker.position, Quaternion.identity);
    }

    IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(5f);
        StopCoroutine(Shoot());
        isShooting = false;
        isrunning = false;
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1f);
        isShooting = true;

    }

    //Attacking mode code goes here...........
    private bool canSeePlayer(float distance)
    {
        bool val = false;
        float castDistance;
       // if (transform.localScale.x > 0)
           // castDistance = distance;
       // else
            castDistance = -distance;



        Vector2 endposition = checker.position + Vector3.right * castDistance;

        RaycastHit2D hit; 

        RaycastHit2D hitBox = Physics2D.Linecast(checker.position, endposition, 1 << LayerMask.NameToLayer("Platform"));

        

        if(hitBox.collider != null)
        {
            lr.SetPosition(1, hitBox.point);
            hit = Physics2D.Linecast(checker.position, hitBox.point , 1 << LayerMask.NameToLayer("Player"));
        }
        else
        {
            lr.SetPosition(1 , endposition);
            hit = Physics2D.Linecast(checker.position, endposition, 1 << LayerMask.NameToLayer("Player"));
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                endposition = checker.position;
            }
            else
            {
                val = false;
                endposition = checker.position + Vector3.right * castDistance;
            }
            Debug.DrawLine(checker.position, hit.point, Color.red);
        }

        return val;
    }
}
