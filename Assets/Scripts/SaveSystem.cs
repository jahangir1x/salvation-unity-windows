using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    bool isSaved = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            

            PlayerPrefs.SetFloat("X", collision.transform.position.x);
            PlayerPrefs.SetFloat("Y", collision.transform.position.y);

            //play anim
            if (!isSaved)
            {
                GetComponentInChildren<Animator>().Play("checkpointTrigger");
                isSaved = true;
            }
            }
    }
}
