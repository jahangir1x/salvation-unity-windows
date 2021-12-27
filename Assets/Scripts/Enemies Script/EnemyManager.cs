using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyArr;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < EnemyArr.Length ; i++)
        {
            if(PlayerPrefs.GetInt(i.ToString()) == 1)// 1 means dead;
            {
                EnemyArr[i].SetActive(false);
            }
            else
            {
                EnemyArr[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
