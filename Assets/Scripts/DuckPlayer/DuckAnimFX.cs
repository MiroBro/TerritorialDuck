using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAnimFX : MonoBehaviour
{
    //public AudioSource[] steps;
    //public AudioSource[] honks;

    public Animator anim;
    public ParticleSystem grassKickFX;

    public void KickGrass()
    {
        grassKickFX.gameObject.SetActive(true);
        grassKickFX.Play();
    }

    public void RollHonkDice()
    {
        float r = Random.Range(0f, 1f);
        if (r > 0.95) anim.SetTrigger("Honk");
    }

    public void PlayStepSound()
    {
        References.Instance.soundHandler.PlayRandomStep();
    }

    public void PlayHonk()
    {
        References.Instance.soundHandler.PlayRandomHonk();
    }
/*    public static void PlayRandomSound(AudioSource[] steps)
    {
        var i = Random.Range(0, steps.Length);
        PlayRandomPitch(steps[i]);
    }

    public static void PlayRandomPitch(AudioSource sound)
    {
        var p = Random.Range(0.8f, 1.4f);
        sound.pitch = p;
        sound.Play();
    }
*/

}
