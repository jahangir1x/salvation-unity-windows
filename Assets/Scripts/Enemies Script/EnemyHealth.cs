using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject blood;
    public Animator anm;
    //public GameObject damageTag, nadeDamageTag;                // bullet damage tag and nade damage tag as a number
    //public Transform tagPosition;                               //position to set active the tag
    public float enemyHealth = 20f;                             // enemy health

    public bool damageEffect;                                   // if damaged then to play the animation
    public bool freezed;                                        // check if freezed
    float deadTimeDelay = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            
            Damage();
        }
    }

    private void Start()
    {

    }

    void Damage()
    {
        //show damage tag
       // GameObject damagedHealth = Instantiate(damageTag, tagPosition.position, Quaternion.identity);
        //damagedHealth.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);
        //reduce enemy health
        enemyHealth -= 10f;
        if (enemyHealth <= 0f)
        {
            //anm.SetBool("Die", true);                                //dead animation true
            Dead();
        }
    }
    // to destroy enemy
    void Dead()
    {
        UIManager.uiUpdater();
        PlayerPrefs.SetInt(transform.GetSiblingIndex().ToString(), 1);
        if(gameObject.CompareTag("CanSwim"))
            anm.Play("Enemy2Hurt");
        else
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
