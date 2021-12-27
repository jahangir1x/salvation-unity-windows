using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if(collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("X", collision.transform.position.x);
            PlayerPrefs.SetFloat("Y", collision.transform.position.y);
        }
    }
}
