using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicasMovement : MonoBehaviour
{
    public float speed = 2f;

    public float hitSpeed = 2f;
    //for random movement
    private Vector3 target1;
    public Transform player;
    Vector3 playerPos;
    public Transform headPos;
    bool isAttack;
    bool isDoingAttack = false;
    Rigidbody2D rb;
    public enum Action{
        standard,
        attack,

    }
    public Action a1;
    void Start()
    {
         //taking random position for movement
        target1 = new Vector3(Random.Range(-5f,7f), headPos.position.y+Random.Range(0.2f,0.8f), 0f);

        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(a1 == Action.standard)
        {
                StartCoroutine(Move());
        }
        
        else if(a1 == Action.attack && !isAttack)
        {
             StartCoroutine(attack());
        }

        if(isDoingAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * (hitSpeed));

            if (Vector3.Distance(transform.position, playerPos) <= 0.1f)
            {
                isAttack = false;

                a1 = Action.standard;

                isDoingAttack = false;
            }
        }
     
    }

    void positionChange()
    {
        target1 = new Vector2(Random.Range(-5f, 7f), Random.Range(0.2f, 0.8f));
    }
     IEnumerator Move(){
       
        if (Vector3.Distance(transform.position, target1) <= 1f)
        {
            positionChange();
            //yield return new WaitForSeconds(2f);
        }
        transform.position = Vector3.Lerp(transform.position, target1, Time.deltaTime * speed);
        yield return null;
        
        // Vector3 dir = target.position - transform.position;
        //  Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        /* if(Vector3.Distance(transform.position,target.position) <= 0.4f){
            GetNextWaypoint();
         }*/
    }


    IEnumerator attack()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.7f);
        isDoingAttack = true;

        playerPos = player.transform.position;


    }

}
