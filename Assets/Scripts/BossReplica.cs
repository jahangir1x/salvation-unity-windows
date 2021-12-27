using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReplica : MonoBehaviour
{
    public GameObject bossPrefab;
    private Transform target1;
    public float replicaAttackTime = 2.0f;
    private float bosstime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        target1 = GameObject.FindGameObjectWithTag("Player").transform;
        Replica();
    }

    private void Update() {

        if (transform.childCount > 3)
        {
            if (replicaAttackTime <= 0.0f)
            {

                ReplicasMovement rp = transform.GetChild(Random.Range(3, transform.childCount)).GetComponent<ReplicasMovement>();

                if (rp.a1 != ReplicasMovement.Action.attack)
                {
                    rp.a1 = ReplicasMovement.Action.attack;

                    //rp.GetPosition(target1.position);
                }

                replicaAttackTime = 3.0f;

                bosstime -= Time.deltaTime;
            }

            replicaAttackTime -= Time.deltaTime;
        }

    }
    
    

    public void Replica()
    {
         GameObject bossRepel1 = Instantiate(bossPrefab,transform.position,Quaternion.identity, transform);
         GameObject bossRepel2 = Instantiate(bossPrefab,transform.position,Quaternion.identity, transform);
         GameObject bossRepel3 = Instantiate(bossPrefab,transform.position,Quaternion.identity, transform);
         GameObject bossRepel4 = Instantiate(bossPrefab,transform.position,Quaternion.identity, transform);
         GameObject bossRepel5 = Instantiate(bossPrefab,transform.position,Quaternion.identity, transform);
    }
}
