using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public LayerMask Grabbale;
    Transform cameraTransform;
    private GameObject pickedUpObj;
    public float speed = 10f;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (pickedUpObj != null)
        {
            float step = speed * Time.deltaTime;

            pickedUpObj.transform.position = new Vector3();

            if (Input.GetKey(KeyCode.Q))
            {
                pickedUpObj.transform.position = Vector3.MoveTowards(pickedUpObj.transform.position, transform.position, step);
            }
            if (Input.GetKey(KeyCode.E))
            {
                pickedUpObj.transform.position = Vector3.MoveTowards(pickedUpObj.transform.position, -transform.position, step);
            }
        }

        //If mouse button down and raycast is over grabbale object set picked up object to item hit.
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, Grabbale))
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5000, Color.red);
                pickedUpObj = hit.collider.gameObject;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            pickedUpObj = null;
        }
    }
}
