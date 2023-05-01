using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text leveltime_1;
    [SerializeField]
    private TMP_Text leveltime_2;
    [SerializeField]
    private TMP_Text leveltime_3;
    [SerializeField]
    private TMP_Text leveltime_4;
    [SerializeField]
    private TMP_Text leveltime_5;

    void Start()
    {
        leveltime_1.text = PlayerPrefs.GetFloat("Level_1").ToString() + "s";
        leveltime_2.text = PlayerPrefs.GetFloat("Level_2").ToString() + "s";
        leveltime_3.text = PlayerPrefs.GetFloat("Level_3").ToString() + "s";
        leveltime_4.text = PlayerPrefs.GetFloat("Level_4").ToString() + "s";
        leveltime_5.text = PlayerPrefs.GetFloat("Level_5").ToString() + "s";
    }
    void Update()
    {
        
    }
}
