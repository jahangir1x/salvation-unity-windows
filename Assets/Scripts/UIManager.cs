using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI[] stones; //1-4

    int scr;
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
        scr = PlayerPrefs.GetInt("Score", 0);
        score.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }

    public static void uiUpdater()
    {
        instance.scr += 5;
        instance.score.text = instance.scr.ToString();
    }
    public static int getScore()
    {
        return instance.scr;
    }

    public void UpdateStoneUI()
    {
        for (int i = 0; i < 4; i++)
        {
            int value = (PlayerPrefs.GetInt("Stone" + (i + 1), 0));

            stones[i].text = value.ToString();
        }
    }
}
