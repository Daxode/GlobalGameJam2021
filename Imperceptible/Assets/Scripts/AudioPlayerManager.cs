using System;
using UnityEngine;

public class AudioPlayerManager: MonoBehaviour
{
    private static AudioPlayerManager instance = null;
    private AudioSource audioo;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return; 
        Destroy(gameObject);
    }

    void Start()
    {
        audioo = GetComponent<AudioSource>();
        audioo.Play();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}