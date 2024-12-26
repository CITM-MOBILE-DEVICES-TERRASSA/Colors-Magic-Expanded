using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    public void PlaySound(string sound)
    {
        Debug.Log("Playing Sound: " + sound);
    }

    public void PlayMusic(string track)
    {
        Debug.Log("Playing Music: " + track);
    }

    public void StopMusic()
    {
        Debug.Log("Stopping Music");
    }
}
