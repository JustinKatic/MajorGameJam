using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisTrigger : MonoBehaviour
{
    public ParticleSystem fx1;
    public ParticleSystem fx2;
    public ParticleSystem fx3;
    public ParticleSystem fx4;
    public AudioSource audioFX;
    public Animator camShake;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            fx1.Play();
            fx2.Play();
            fx3.Play();
            fx4.Play();
            audioFX.Play();
            camShake.Play("CamShake");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fx1.Play();
            fx2.Play();
            fx3.Play();
            fx4.Play();
            audioFX.Play();
            camShake.Play("CamShakeBig");
        }
    }
}
