using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public healthBar healthBar;

    public GameObject deathVFX;
    public LayerMask BossMask;
    public float playerHealth = 200f;
    public GameObject idle;
    public bool playerOn;
    bool availableDamage;

    public LayerMask enemy1Mask;
    public LayerMask enemy2Mask;
    public LayerMask enemy3Mask;
    public LayerMask enemy4Mask;
    public LayerMask enemy5Mask;
    public LayerMask enemy6Mask;
    public LayerMask enemy7Mask;

    [Header("camera shake")]
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;
    public Collider2D col;
    bool touchedBoss, touchedEnemy1, touchedEnemy2, touchedEnemy3, touchedEnemy4, touchedEnemy5, touchedEnemy6, touchedEnemy7;

    [Header("Flash")]
    // The SpriteRenderer that should flash.
    public SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    [Space]
    public LayerMask jombieMask;

    public GameObject coinPickupVFX;

    int coroutineOnce = 0;
    //another for spider.. damage value different..
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            TakeDamage(20f); // enemy missile.. 

        }

    }

    private void Start()
    {
        healthBar.SetMaxHealth(playerHealth);
        playerOn = true;
        availableDamage = true;
        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        Damage();
    }

    public void TakeDamage(float damageAmount)
    {
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

        Flash();

        playerHealth -= damageAmount;
        healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            playerOn = false;

            // idle.GetComponent<SpriteRenderer>().enabled = false;
            //  idle.GetComponent<Animator>().enabled = false;

            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            col.enabled = false;

            StartCoroutine(HandleDeath());

            // audio_Manager.instance.Play("dead");

            CinemachineShake.Instance.ShakeCamera(50, 0.2f);
        }
    }

    IEnumerator HandleDeath()
    {
        GameManager.instance.isGameOver = true;

        //GameObject playerVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);

        // Destroy(playerVFX, 4f);

        // yield return new WaitForSeconds(1.1f);

        //   TransitionManager.instance.PlayANimLoad("transition");


        yield return new WaitForSeconds(0.7f);


    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }

    void Damage()
    {
        touchedEnemy1 = Physics2D.OverlapCircle(transform.position, 1f, enemy1Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy2 = Physics2D.OverlapCircle(transform.position, 1f, enemy2Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy3 = Physics2D.OverlapCircle(transform.position, 1f, enemy3Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy4 = Physics2D.OverlapCircle(transform.position, 1f, enemy4Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy5 = Physics2D.OverlapCircle(transform.position, 1f, enemy5Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy6 = Physics2D.OverlapCircle(transform.position, 1f, enemy6Mask, Mathf.Infinity * -1, Mathf.Infinity);
        touchedEnemy7 = Physics2D.OverlapCircle(transform.position, 1f, enemy7Mask, Mathf.Infinity * -1, Mathf.Infinity);

        touchedBoss = Physics2D.OverlapCircle(transform.position, 1f, BossMask, Mathf.Infinity * -1, Mathf.Infinity);

        if ((touchedEnemy1 || touchedEnemy2 || touchedEnemy3 || touchedEnemy4 || touchedEnemy5 ||
            touchedEnemy6 || touchedEnemy7 || touchedBoss) && availableDamage)
        {
            if (touchedEnemy1)
            {
                StartCoroutine(TakeDamg(10f));
            }
            if (touchedEnemy2)
            {
                StartCoroutine(TakeDamg(5f));
            }
            if (touchedEnemy3)
            {
                StartCoroutine(TakeDamg(10f));
            }
            if (touchedEnemy4)
            {
                StartCoroutine(TakeDamg(5f));
            }
            if (touchedEnemy5)
            {
                StartCoroutine(TakeDamg(10f));
            }
            if (touchedEnemy6)
            {
                StartCoroutine(TakeDamg(5f));
            }
            if (touchedEnemy7)
            {
                StartCoroutine(TakeDamg(10f));
            }
            if (touchedBoss)
            {
                StartCoroutine(TakeDamg(80f));
            }

        }

    }

    IEnumerator TakeDamg(float damage)
    {
        TakeDamage(damage);
        availableDamage = false;
        yield return new WaitForSeconds(1f);
        availableDamage = true;

    }
}
