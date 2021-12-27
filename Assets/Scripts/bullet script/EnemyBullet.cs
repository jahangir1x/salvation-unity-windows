using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed = 7f, lifeTime = 4f;            // bullet speed and life time
    Vector2 target;                             // terget to shoot - PlayerPosition
    //Animator anim;                       // bullet destroy animaiton
    bool colliding;                             //check if colliding
    float TimeToDistroy = .3f;                  // destroy time delay - time of animation end

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        colliding = false;
        StartCoroutine(CountDownTimer());       //to destroy as soon as animation ends
    }

    // while colliding
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            colliding = true;
            StartCoroutine(VanishBullet(TimeToDistroy));
        }

    }
    private void OnEnable()
    {
        colliding = false;
        StartCoroutine(CountDownTimer());       //to destroy as soon as animation ends
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 bulletEndPosition = transform.position;
        bulletEndPosition.x = transform.position.x + 10 * transform.localScale.x;
        // if not collide to move towards playerposition
        if (colliding == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, bulletEndPosition, speed * Time.deltaTime);
        }
        // start animation and destroy as soon as animation ends
        if (target.x == transform.position.x && target.y == transform.position.y)
        {
            //anim.SetBool("Die", true);
            float time = 1f;
            StartCoroutine(VanishBullet(time));
        }
    }
    //to destroy as soon as animation ends
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(lifeTime);

        StartCoroutine(VanishBullet(TimeToDistroy));
    }
    //to destroy as soon as animation ends
    IEnumerator VanishBullet(float destroyTime)
    {
        
        yield return new WaitForSeconds(destroyTime);
        //anim.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}

