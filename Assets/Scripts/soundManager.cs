using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {

        source = GetComponent<AudioSource>();
        // singleton AudioSource
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
