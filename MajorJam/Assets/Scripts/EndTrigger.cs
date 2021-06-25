using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public Animator fadeAnim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Waitforsec());
        }
    }

    IEnumerator Waitforsec()
    {
        fadeAnim.Play("FadeToBlack");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");

    }
}
