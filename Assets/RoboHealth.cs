using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboHealth : MonoBehaviour
{
    public GameObject Tbar1;
    public GameObject Robo2;
    float health = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Damage();
        }
    }

    void Damage()
    {
        health -= 10f;
        if(health <= 0)
        {
            Destroy(Tbar1);
            Destroy(Robo2);
            Destroy(gameObject);
        }
    }
}
