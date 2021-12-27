using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    Transform player;
    public GameObject bulletFlying;                                // bullet to intentiate
    public Transform bulletPossition;                              // bullet starting position
    public GameObject tankHead;

    int timeBetweenShoots = 3;                                      // time between shooting
    bool canShoot;                                                  //if ready to shoot 
    public float distance;                                          //distance between player and enemy

    float desiredScale = 1.5f;                                      //desired scale of tank
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //will be able to shoot from start
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        tankHead.transform.eulerAngles = new Vector3(tankHead.transform.eulerAngles.x, tankHead.transform.eulerAngles.y, RotaitonofZ(bulletPossition.position));
        //to check distance between player and tank
        distance = Vector2.Distance(player.position, transform.position);
        // check if enough close to shoot
        if (distance <= 15f && player.position.y - 3f <= transform.position.y)
        {
            //if able to shoot
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (player.position.x <= transform.position.x)
        {
            transform.localScale = new Vector2(desiredScale, desiredScale);
        }
        else
        {
            transform.localScale = new Vector2(-desiredScale, desiredScale);
        }
    }

    // to shoot
    IEnumerator Shoot()
    {
        canShoot = false;                                                   // to prevent continously shooting
        float desiredScale = .7f;                                           //desired scale or tank bullet
        yield return new WaitForSeconds(timeBetweenShoots);
        //setting active bullet
        GameObject newBullet = Instantiate(bulletFlying , bulletPossition.position, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, newBullet.transform.eulerAngles.z + RotaitonofZ(bulletPossition.position));
        
        //checking if need to flip and flip
        if (player.position.x > transform.position.x)
        {
            newBullet.transform.localScale = new Vector3(desiredScale, desiredScale, 1);
        }
        else
        {
            newBullet.transform.localScale = new Vector3(-desiredScale, desiredScale, 1);
        }
        // making ready to shoot again
        canShoot = true;
    }

    float RotaitonofZ(Vector2 position)
    {
        float x, y;
        x = player.transform.position.x - position.x;
        y = player.transform.position.y - position.y;

        float rotation = Mathf.Atan(y / x);

        rotation *= 180 / Mathf.PI;

        return rotation;
    }
}
