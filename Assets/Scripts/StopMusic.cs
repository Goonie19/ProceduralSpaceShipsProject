using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public void DestroyMusic()
    {
        GameObject music = GameObject.Find("MusicManager");

        Destroy(music);
    }
}
