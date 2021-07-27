using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        source.Play();
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyMusic()
    {
        Destroy(gameObject);
    }
}
