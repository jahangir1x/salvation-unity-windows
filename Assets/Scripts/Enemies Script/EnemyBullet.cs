using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float lifeTime = 4f;            // bullet speed and life time

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    // while colliding
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VanishBullet();
        }

    }
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(lifeTime);

        VanishBullet();
    }
    void VanishBullet()
    {
        Destroy(gameObject);
    }
}

