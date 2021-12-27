using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;              // set the platform in stationary mode
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GameManagerRocky.PlayerLayer)
        {
            animator.Play(GameManagerRocky.PlatformShakeAnimID);   // play shake animation when the platform collides with player.
            StartCoroutine(FallPlatform(fallDelay));          // the platform should fall after fallDelay.
        }
    }


    IEnumerator FallPlatform(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        animator.Play(GameManagerRocky.NullAnimID);            // set the animation to null, as it should not show any animation.
        rb.bodyType = RigidbodyType2D.Dynamic;            // allow the platform to fall
        
        yield return new WaitForSeconds(1f);              // start vanishing after some delay
        animator.Play(GameManagerRocky.PlatformVanishAnimID);  // start playing "vanishing" animation

        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);                         // remove gameobject after some delay
    }
}
