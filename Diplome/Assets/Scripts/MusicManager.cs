using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour { 

    private AudioSource music;
    private void Awake()
    {
        music = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
        //Debug.Log(music);
    }
    public void MuteOnMusic()
    {
        if(music.mute)
        {
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }
    }
}
