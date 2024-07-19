using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioSource[] wetHitAS;
    public AudioSource[] leavesAS;
    public AudioSource[] pickUpAS;
    public AudioSource[] levelUpAS;
    public AudioSource findItemAS;
    public AudioSource shimmerAS;
    public AudioSource[] primalHonkAS;

    public AudioSource[] steps;
    public AudioSource[] honks;
    // Start is called before the first frame update
    public AudioSource music;
    public static float musicOrigVol;

    public AudioSource uiClick;
    public AudioSource uiHover;
    public AudioSource leaf;
    public AudioSource fabric;

    public AudioSource[] quacks;

    private float lvlVol;
    private float findItemVol;
    private float shimmerVol;

    public HitSoundAssets hitSoundAssets;

    public AudioSource choir;
    public AudioSource evil;

    public GameObject buildHandler;

    public AudioSource errorAS;
    public AudioSource placeAS;
    public AudioSource buyAS;

    public void PlayBuySound()
    {
        buyAS.Play();
    }
    public void PlayBuildSound()
    {
        placeAS.Play();
    }
    public void PlayErrorSound()
    {
        errorAS.Play();
    }
    public void PlayClickBtn()
    {
        PlayRandomPitch(uiClick);
    }

    public void PlayHoverBtn()
    {
        PlayRandomPitch(uiHover);
    }

    public void PlayLeaf()
    {
        PlayRandomPitch(leaf);
    }
    private void Awake()
    {
        musicOrigVol = music.volume;
        lvlVol = levelUpAS[0].volume;
        findItemVol = findItemAS.volume;
        findItemVol = findItemAS.volume;
        shimmerVol = shimmerAS.volume;
    }
    public void SilenceMusic()
    {
        music.volume = 0;
        buildHandler.SetActive(false);
        //StartCoroutine(ChangeVolume(music, music.volume, 0, 0.4f));
    }

    public void EnableBuildMusic()
    {
        buildHandler.SetActive(true);
    }

    public void EnableMusic()
    {
        StartCoroutine(ChangeVolume(music, 0, musicOrigVol, 0.5f));
        //StopLvlUpSound();
        //music.volume = musicOrigVol;
    }

    public void TurnOnMusic()
    {
        music.volume = .1f;
        music.Play();
    }

    public void PlayRandomStep()
    {
        PlayRandomSoundAndPitch(steps);
    }

    public void PlayRandomHonk()
    {
        PlayRandomSoundAndPitch(honks);
    }

    public void PlayPrimalHonk()
    {
        PlayRandomSound(primalHonkAS);
    }

    public void PlayNewItem()
    {
        findItemAS.Play();
    }

    public void StopPlayNewItem()
    {
        findItemAS.Stop();
    }

    public void PlayLevelUpSound()
    {
        levelUpAS[0].volume = lvlVol;
        shimmerAS.volume = shimmerVol;
        shimmerAS.Play();
        PlayRandomSound(levelUpAS);
    }

    public void StopLvlUpSound()
    {
        StartCoroutine(ChangeVolume(levelUpAS[0], levelUpAS[0].volume, 0, 0.5f));
        StartCoroutine(ChangeVolume(shimmerAS, shimmerAS.volume, 0, 0.5f));
    }

    public void PlayFabricSound()
    {
        PlayRandomPitch(fabric);
    }

    public void PlayPickUp()
    {
        PlayRandomSoundAndPitch(pickUpAS);
    }
    public void PlayRandomLeave()
    {
        PlayRandomSoundAndPitch(leavesAS);
    }
    public static void PlayRandomSoundAndPitch(AudioSource[] steps)
    {
        var i = Random.Range(0, steps.Length);
        PlayRandomPitch(steps[i]);
    }

    public static void PlayRandomSound(AudioSource[] steps)
    {
        var i = Random.Range(0, steps.Length);
        steps[i].Play();
    }

    public static void PlayRandomPitch(AudioSource sound)
    {
        var p = Random.Range(0.8f, 1.4f);
        sound.pitch = p;
        sound.Play();
    }

    public static void PlayRandomSoundAndPitchAndVolume(AudioSource[] steps, float minVolume, float maxVolume)
    {
        var i = Random.Range(0, steps.Length);
        var sound = steps[i];
        var p = Random.Range(0.8f, 1.4f);
        var v = Random.Range(minVolume, maxVolume);
        sound.pitch = p;
        sound.volume = v;
        sound.Play();
    }

    public void PlayRandomQuack(float minVolume, float maxVolume)
    {
        PlayRandomSoundAndPitchAndVolume(quacks, minVolume, maxVolume);
    }

    public void PlayChoir()
    {
        if (!choir.isPlaying)
        {
            evil.Stop();
            choir.Play();
        }
       // StartCoroutine(ChangeVolume(choir, 0, 1, 0.5f));
    }

    public void PlayEvil()
    {
        if (!evil.isPlaying)
        {
            choir.Stop();
            evil.Play();
        }
        //StartCoroutine(ChangeVolume(evil, 0, 1, 0.5f));
    }

    public void StopChoiceMusic()
    {
        choir.Stop();
        evil.Stop();
    }

    IEnumerator ChangeVolume(AudioSource aus, float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            aus.volume = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        aus.volume = v_end;
    }
}



/*
   public void PlayRandomHit()
   {
       PlayRandomSoundAndPitch(wetHitAS);
   }


   public void PlayHit(AudioSource auS)
   {
       PlayRandomPitch(auS);
   }
   */

//levelUpAS[0].volume = 0;
//levelUpAS[0].gameObject.SetActive(true);
//levelUpAS[0].Play();
//StartCoroutine(ChangeVolume(levelUpAS[0], 0, lvlVol, 0.4f));