using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI[] stones; //1-4


    [SerializeField] TextMeshProUGUI score;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            int value = (PlayerPrefs.GetInt("Stone" + (i + 1), 0));
            
            stones[i].text = value.ToString();
        }

        score.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
