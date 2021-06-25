using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBody : MonoBehaviour
{
    public void AddRigidBodyTooObj()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
    }
}
