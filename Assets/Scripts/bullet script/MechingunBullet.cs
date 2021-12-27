using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechingunBullet : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Timer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Platform"))
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(2300f * transform.localScale.x * Time.deltaTime, rb.velocity.y);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
