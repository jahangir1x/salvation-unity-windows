using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBullet : MonoBehaviour
{
    [SerializeField] private float bulletActiveTime = 4f;

    private void OnEnable()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(bulletActiveTime);
        gameObject.SetActive(false);
    }

}
