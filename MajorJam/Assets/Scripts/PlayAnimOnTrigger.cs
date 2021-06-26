using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnTrigger : MonoBehaviour
{
    public Animator anim;
    public string animToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            anim.Play(animToPlay);
    }
}
