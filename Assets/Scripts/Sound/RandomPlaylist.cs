using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaylist : MonoBehaviour
{

    public AudioClip[] music;
    public AudioSource audioS;
    private void Start()
    {
        if (!audioS.playOnAwake)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        audioS.clip = music[Random.Range(0, music.Length)];
        audioS.Play();
        Invoke("PlayNextSong", audioS.clip.length);
    }
}
