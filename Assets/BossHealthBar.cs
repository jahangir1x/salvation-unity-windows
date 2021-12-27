using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;


    public Gradient grad;

    public Image fill;
    public void SetMaxHealth(float Bosshealth)
    {
        slider.maxValue = Bosshealth;
        slider.value = Bosshealth;
        fill.color = grad.Evaluate(1f);

    }

    public void SetHealth(float Bosshealth)
    {
        slider.value = Bosshealth;

        fill.color = grad.Evaluate(slider.normalizedValue);
    }
}
