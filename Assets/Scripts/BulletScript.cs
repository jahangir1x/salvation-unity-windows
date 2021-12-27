using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float speed = 50f;          // bullet speed;
    float lifeTime = 2f;        // bullet lifetime;
    Rigidbody2D rb;             //bullet rigidbody
    //Animator anim;       //bullet animator
    float bulletVanishTime = .05f;

    bool shouldStopMovement = false; //to check if collided or not ;
                                     //bullet hit something

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Destroy(gameObject);
    }
    public void VanishBullet()
    {
        //anim.enabled = true; //enable distroy animation
        //anim.SetBool("Die", true);
        shouldStopMovement = true; // make it true cause it've collided
        StartCoroutine(VanishBulletRoutine()); // Vanish bullet within the animation end
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        rb.velocity = Vector2.zero;
        //StartCoroutine(CountDownTimer()); // start countdown for bullet lifetime
    }

    private void OnEnable()
    {
        resetScript();
    }
    // Update is called once per frame
    void Update()
    {
        //When its not collided to move forword;
        if (!shouldStopMovement)
        {
            Vector2 Velocity = new Vector2(speed, 0f);
            Velocity.x *= transform.localScale.x;
            rb.velocity = Velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    // to destroy as soon as lifetime ends
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(VanishBulletRoutine());
    }
    // to vanish bullet
    IEnumerator VanishBulletRoutine()
    {
        yield return new WaitForSeconds(bulletVanishTime);
        //anim.SetBool("Die", false);
        Destroy(gameObject);
    }

    void resetScript()
    {
        shouldStopMovement = false;
        StartCoroutine(CountDownTimer()); // start countdown for bullet lifetime
    }
}
