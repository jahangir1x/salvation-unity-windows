using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotz;
    public float rotationspeed;
    public bool clockwiseRotation;

    void Update()
    {
        if (clockwiseRotation)
        {
            rotz += Time.deltaTime * rotationspeed;
        }
        else
        {
            rotz += -Time.deltaTime * rotationspeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
