using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundAssets : MonoBehaviour
{
    public AudioClip[] genericHits;
    public AudioClip[] genericLeave;

    public AudioClip[] catHits;
    public AudioClip[] catLeave;

    public AudioClip[] girlHits;
    public AudioClip[] girlLeave;

    public AudioSource[] hitAS;
    public AudioSource[] leaveAS;

    private int hitCount;
    private int leaveCount;

    public float hitMaxVolume;
    public float hitMinVolume;

    public enum HitType
    {
        Default,
        Cat,
        Girl,
    }

    private void PlayHitAS(AudioClip[] ac)
    {
        var randAC = ac[Random.Range(0, ac.Length)];
        hitCount = (hitCount + 1) % hitAS.Length;
        hitAS[hitCount].clip = randAC;

        RandomVolAndPitch(hitAS[hitCount]);
    }

    private void PlayLeaveAS(AudioClip[] ac)
    {
        var randAC = ac[Random.Range(0, ac.Length)];
        leaveCount = (leaveCount + 1) % leaveAS.Length;
        leaveAS[leaveCount].clip = randAC;

        RandomVolAndPitch(leaveAS[leaveCount]);
    }

    private void RandomVolAndPitch(AudioSource aus)
    {
        var p = Random.Range(0.8f, 1.2f);
        var v = Random.Range(hitMinVolume, hitMaxVolume);

        aus.pitch = p;
        aus.volume = v;
        aus.Play();
    }

    public void PlayHit(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Default:
                PlayHitAS(genericHits);
                break;
            case HitType.Cat:
                PlayHitAS(catHits);
                break;
            case HitType.Girl:
                PlayHitAS(girlHits);
                break;
            default:
                break;
        }
    }

    public void PlayLeave(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Default:
                PlayLeaveAS(genericLeave);
                break;
            case HitType.Cat:
                PlayLeaveAS(catLeave);
                break;
            case HitType.Girl:
                PlayLeaveAS(girlLeave);
                break;
            default:
                break;
        }
    }
}
