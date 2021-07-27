using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;

    private bool init = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        source.Play();
        DontDestroyOnLoad(gameObject);
    }
}
