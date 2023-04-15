using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour { 
   
    [SerializeField]
    private Slider musicslider;
    private AudioSource music;
    private void Awake()
    {
        music = GameObject.Find("GameManager").GetComponent< AudioSource >();
        musicslider.value = PlayerPrefs.GetFloat("musicvalue");
    }
    private void Update()
    {
        CheckMusicSlider();
        music.volume = PlayerPrefs.GetFloat("musicvalue");
    }
    public void CheckMusicSlider()
    {
        PlayerPrefs.SetFloat("musicvalue",musicslider.value);
    }
}
