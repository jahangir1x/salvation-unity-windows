using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform Shootpos;
    public Transform player;
    public float speed,time;
    public GameObject fireBall;
    private bool isShooting;
    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        boss = gameObject.GetComponent<Boss>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShooting)
        {
            StartCoroutine(Shooting());

        }
    }

    private IEnumerator Shooting()
    {
        isShooting = true;

        GameObject newfireball= Instantiate(fireBall, Shootpos.position, Quaternion.identity);
        Vector2 dir = player.position - transform.position;
        newfireball.GetComponent<Rigidbody2D>().AddForce(dir * speed * Time.deltaTime, ForceMode2D.Impulse);

        //newfireball.GetComponent<Rigidbody2D>().velocity = new Vector2(-boss.dir* transform.localScale.x*speed * Time.deltaTime, 0f);

        yield return new WaitForSeconds(time);

        isShooting = false;


    }
}
