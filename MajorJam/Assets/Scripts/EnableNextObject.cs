using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextObject : MonoBehaviour
{
    public GameObject objectToEnable;
   

    public void EnableObject()
    {

        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }
}
