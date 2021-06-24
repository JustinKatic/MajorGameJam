using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocks : MonoBehaviour
{
    public LayerMask targetLayer;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.transform.TransformDirection(Vector3.forward), out hit, 3f, targetLayer))
        {
            Debug.Log(hit.transform.name);
        }
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(Vector3.forward) * 1, Color.red);
    }
}
