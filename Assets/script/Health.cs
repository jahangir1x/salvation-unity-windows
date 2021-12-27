using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(float maxHealth)
    {


        slider.maxValue = maxHealth;
    }
    public void SetHealth(float Health)
    {
        slider.value=Health;
        
    }
}
