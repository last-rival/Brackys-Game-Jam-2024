using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviourInstance<SoundManager>
{
    public AudioClip pickup;
    public AudioClip jump;
    public AudioClip set;
    public AudioSource walk;
    public AudioSource sfx;
    public AudioSource jumpSource;
    

    public void PlayPickUp()
    {
        sfx.clip = pickup;
        sfx.Play();
    }

    public void PlayJump()
    {
        jumpSource.clip = jump;
        jumpSource.Play();
    }

    public void PlaySet()
    {
        sfx.clip = set;
        sfx.Play();
    }

    public void SetWalk(bool walking)
    {
        if (walk.isPlaying == false)
        {
            walk.Play();
        }

        if (walking)
        {
            walk.UnPause();
        }
        else
        {
            walk.Pause();
        }
    }

}
