using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankHealth : MonoBehaviour
{
    public GameObject tankHead;
    public Animator TankAnim;                           // tank animation
    public GameObject effect;                           // tand destroy smoke effect
    EnemyTank enemyTank;                                // enmey tank script

    //public GameObject damageTag;                        //damage tag
    //public GameObject nadeDamageTag;                    //nade damage tag
    //public Transform tagPosition;                       // nade damage tag position to set active


    public float enemyHealth = 100f;                    // tank health
    float bulletDamage = 10;                            //damage amount through granade
    float granadeDamage = 80;                           // damage amount through bullet

    void Start()
    {
        //Debug.Log(transform.GetSiblingIndex());
        enemyTank = GetComponent<EnemyTank>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
            Damage();
    }
    // taking damage from granade 
    void nadeDamage()
    {
        //GameObject damagedHealth = Instantiate(nadeDamageTag, tagPosition.position, Quaternion.identity);
       // damagedHealth.GetComponent<Rigidbody2D>().velocity = velocityofNade;
        enemyHealth -= granadeDamage;                           // take damage
        if (enemyHealth <= 0f)
        {
            Dead();
        }
    }
    //taking damage through bullet
    void Damage()
    {
        //GameObject damagedHealth = Instantiate(damageTag, tagPosition.position, Quaternion.identity);
        enemyHealth -= bulletDamage;                            // take damage 
        if (enemyHealth <= 0f)
        {
            Dead();
        }
    }
    // destroy 
    void Dead()
    {
        tankHead.SetActive(false);
        enemyTank.enabled = false;                              // to disable enemy tank script to stop all activity
        TankAnim.enabled = true;                                //destroy animation play
        effect.SetActive(true);                                 //smoke effect on;
        PlayerPrefs.SetInt(transform.GetSiblingIndex().ToString(), 1);
        StartCoroutine(SmokeOff());                             // to turn of smoke effect after certain time ;
    }
    // to turn of the smoke effect;
    IEnumerator SmokeOff()
    {
        yield return new WaitForSeconds(15f);
        effect.SetActive(false);
    }

}
