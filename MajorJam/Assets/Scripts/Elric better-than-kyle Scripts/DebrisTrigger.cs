using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisTrigger : MonoBehaviour
{
    public ParticleSystem fx1;
    public ParticleSystem fx2;
    public ParticleSystem fx3;
    public ParticleSystem fx4;
    public ParticleSystem fx5;
    public ParticleSystem fx6;
    public ParticleSystem fx7;
    public ParticleSystem fx8;
    public AudioSource audioFX;
    public Animator camShake;

    public void PlayFX()
    {
        fx1.Play();
        fx2.Play();
        fx3.Play();
        fx4.Play();
        fx5.Play();
        fx6.Play();
        fx7.Play();
        fx8.Play();
        audioFX.Play();
        camShake.Play("CamShake");
    }
    public void PlayWallFX()
    {
        fx1.Play();
        fx2.Play();
        fx3.Play();
        fx4.Play();
        fx5.Play();
        fx6.Play();
        fx7.Play();
        fx8.Play();
        audioFX.Play();
        camShake.Play("CamShakeBig");
    }
}
