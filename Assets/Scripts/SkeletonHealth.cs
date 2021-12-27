using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{

    [SerializeField]
    public float skeletonHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
            Damage(10f);
    }

    public void Damage(float damageAmount)
    {

        skeletonHealth -= damageAmount;

        //healthBar.SetHealth(bossHealth);

        if (skeletonHealth <= 0f)
        {
            // GameManager.instance.enemyCount--;

            //CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

            //Instantiate(deathVXF, transform.position, Quaternion.identity);

            //GameObject bloodStain = Instantiate(deathBloodStain, transform.position, Quaternion.identity);
            // bloodStain.transform.localRotation = Quaternion.Euler(bloodStain.transform.localRotation.x, bloodStain.transform.localRotation.y,
            //     Random.Range(0, 360));

            Destroy(gameObject);
        }
/*        else
            CinemachineShake.Instance.ShakeCamera(shakeIntensity / 2.5f, shakeTime / 2.5f);*/
    }
}
