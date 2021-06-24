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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fx1.Play();
            fx2.Play();
            fx3.Play();
            fx4.Play();
            audioFX.Play();
        }
    }
}
