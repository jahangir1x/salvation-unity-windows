using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tbar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(10000f);
        }
    }
}
