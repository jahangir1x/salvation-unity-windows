using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            if (collision.CompareTag("Boss") || collision.CompareTag("Enemy") || collision.CompareTag("Platform") || collision.CompareTag("EnemyBullet"))
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.CompareTag("EnemyBullet"))
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Platform") || collision.CompareTag("PlayerBullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}
