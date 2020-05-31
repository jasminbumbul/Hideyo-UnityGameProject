using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSetting : MonoBehaviour
{
    public AudioSource BackgroundMusic;

    public void lowVolume()
    {
        BackgroundMusic.volume = 0.01f;
    }
    public void mediumVolume()
    {
        BackgroundMusic.volume = 0.05f;
    }
    public void highVolume()
    {
        BackgroundMusic.volume = 0.1f;
    }
}
