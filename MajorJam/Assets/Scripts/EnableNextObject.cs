using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextObject : MonoBehaviour
{
    public GameObject[] objectToEnable;


    public void EnableObject()
    {
        if (objectToEnable[0] != null)
        {
            for (int i = 0; i < objectToEnable.Length; i++)
            {
                objectToEnable[i].SetActive(true);
            }
        }
    }
}
