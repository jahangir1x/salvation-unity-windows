using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    public GameObject[] Electricity;

    bool isNotActive = true;
    bool isActive = true;

    public float activeTime;
    public float deActiveTime;

     void Start()
    {
        StartCoroutine(Toggle());
    }



    IEnumerator Toggle()
    {
        for (int i = 0; i < 4; i++) Electricity[i].SetActive(true);

        yield return new WaitForSeconds(activeTime);

        for(int i = 0; i < 4; i++) Electricity[i].SetActive(false);

        yield return new WaitForSeconds(deActiveTime);

        StartCoroutine(Toggle());

    } 
    
 /*   IEnumerator Deactivator()
    {
        isNotActive = false;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 4; i++) Electricity[i].SetActive(false);
        isNotActive = true;

    }
    IEnumerator Activator()
    {
        isActive = false;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++) Electricity[i].SetActive(true);
        isActive = true;
    }*/
}
