using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotRotation : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public int shooting_speed;
    public float rotz;
    public float rotationspeed;
    public bool robot;
    private int count = 0 ;
    public float shootTime=.4f;
    public float pauseTime = .3f;
    public int x,y;
    private Vector3 objHeight;

    public Transform target;
    void Start()
    {
        objHeight.y = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        objHeight.y = objHeight.y - 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotz < x && count%2==0)
        {
            rotz += Time.deltaTime * rotationspeed;
        }
        if (rotz >= x)
            count++;
        if(rotz >y && count%2!=0)
        {
            rotz += -Time.deltaTime * rotationspeed;
        }
        if (rotz <= y)
            count++;
        if(shootTime <= 0.0f)
        {
            
            StartCoroutine(shoot());
            shootTime = .4f;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotz);
        shootTime -= Time.deltaTime;
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(pauseTime);
        GameObject bullet1 = (GameObject) Instantiate(bullet,pos.position,Quaternion.identity);
        if(robot == true){
             bullet1.GetComponent<Rigidbody2D>().AddForce(-transform.up * shooting_speed,ForceMode2D.Impulse);
         }   
         else{
             bullet1.GetComponent<Rigidbody2D>().AddForce( -transform.right * shooting_speed,ForceMode2D.Impulse); 
         }
       

        Destroy(bullet1,3f);
    }

}

