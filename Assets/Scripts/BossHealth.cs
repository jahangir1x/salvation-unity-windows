using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    float Health;
    [SerializeField]
    public  float bossHealth;

    public float playerBulletDamageValue;


    Boss boss;
    [SerializeField] healthBar bossHealthBar;

    //public healthBar healthBar;

    float maxHealth;
    bool damageGiven;

    float shakeIntensity = 3;
    float shakeTime = 0.1f;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = bossHealth;
        bossHealthBar.SetMaxHealth(bossHealth);

        boss = gameObject.GetComponent<Boss>();

        sr = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Damage(playerBulletDamageValue);

        }
    }


    public void Damage(float damageAmount)
    {

        bossHealth -= damageAmount;
        bossHealthBar.SetHealth(bossHealth);

        //healthBar.SetHealth(bossHealth);

        if (bossHealth <= 0f)
        {
           // GameManager.instance.enemyCount--;

            CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

            //Instantiate(deathVXF, transform.position, Quaternion.identity);

            //GameObject bloodStain = Instantiate(deathBloodStain, transform.position, Quaternion.identity);
           // bloodStain.transform.localRotation = Quaternion.Euler(bloodStain.transform.localRotation.x, bloodStain.transform.localRotation.y,
           //     Random.Range(0, 360));

           // Destroy(gameObject);
        }
        else
            CinemachineShake.Instance.ShakeCamera(shakeIntensity / 2.5f, shakeTime / 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

        sr.color = Color.Lerp(Color.white, Color.red, (maxHealth - bossHealth) / maxHealth);


        if (bossHealth > 700f && bossHealth <= 1000f)
        {
            boss.bossStage = Boss.BossStage.SecondStage;
        }
        if(bossHealth > 500f && bossHealth <= 700f)
        {
            boss.bossStage = Boss.BossStage.ThirdStage;
        }
        if(bossHealth > 0f && bossHealth <= 500f)
        {
            boss.bossStage = Boss.BossStage.FourthStage;
        }

        if(bossHealth <= 0f  && boss.bossStage != Boss.BossStage.DeathStage)
        {
            boss.isover = 1;

            boss.bossStage = Boss.BossStage.None;
        }

    }

}
