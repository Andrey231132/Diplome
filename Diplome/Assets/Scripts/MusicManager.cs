using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour { 
   
    [SerializeField]
    private AudioSource music;
    private void Awake()
    {
    
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
