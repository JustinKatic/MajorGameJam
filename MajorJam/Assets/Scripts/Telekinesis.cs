using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Telekinesis : MonoBehaviour
{
    public LayerMask Grabbale;
    Transform cameraTransform;
    private GameObject pickedUpObj;
    private Rigidbody pickedUpObjRB;
    public float objFollowSpeed = 4f;
    public float pullPushSpeed = 4f;


    public Transform telekObj;
    float telekObjZPos;

    PlayerInput _input;
    InputActionMap _playerMovement;
    public float rotateSpeed = 100;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        _input = GetComponent<PlayerInput>();
        _playerMovement = _input.actions.FindActionMap("Player");
    }

    private void FixedUpdate()
    {
        if (pickedUpObj != null)
        {
            float step = objFollowSpeed * Time.deltaTime;
            pickedUpObj.transform.position = Vector3.MoveTowards(pickedUpObj.transform.position, telekObj.position, step);


            if (Input.GetKey(KeyCode.Q))
            {
                telekObjZPos -= Time.deltaTime * pullPushSpeed;
                telekObj.localPosition = new Vector3(0, 0, telekObjZPos);
            }
            if (Input.GetKey(KeyCode.E))
            {
                telekObjZPos += Time.deltaTime * pullPushSpeed;
                telekObj.localPosition = new Vector3(0, 0, telekObjZPos);
            }
            //rotate left
            if (Input.GetKey(KeyCode.A))
                pickedUpObj.transform.RotateAround(telekObj.transform.position, transform.up, rotateSpeed * Time.deltaTime);
            //rotate right
            if (Input.GetKey(KeyCode.D))
                pickedUpObj.transform.RotateAround(telekObj.transform.position, -transform.up, rotateSpeed * Time.deltaTime);
            //rotate up
            if (Input.GetKey(KeyCode.W))
                pickedUpObj.transform.RotateAround(telekObj.transform.position, transform.right, rotateSpeed * Time.deltaTime);
            //roate down
            if (Input.GetKey(KeyCode.S))
                pickedUpObj.transform.RotateAround(telekObj.transform.position, -transform.right, rotateSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        //If mouse button down and raycast is over grabbale object set picked up object to item hit.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, Grabbale))
            {
                if (hit.transform.GetComponent<BuildingBlocks>().grabbable == false)
                    return;
                DisableAction();
                pickedUpObj = hit.collider.gameObject;
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5000, Color.red);

                pickedUpObjRB = pickedUpObj.transform.GetComponent<Rigidbody>();
                pickedUpObjRB.useGravity = false;
                pickedUpObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                float distBetweenPickedUpObjAndPlayer = Vector3.Distance(pickedUpObj.transform.position, transform.position);
                telekObj.localPosition = new Vector3(0, 0, distBetweenPickedUpObjAndPlayer);

                telekObjZPos = distBetweenPickedUpObjAndPlayer;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (pickedUpObj != null)
            {
                pickedUpObjRB.useGravity = true;
                pickedUpObjRB.constraints = RigidbodyConstraints.None;
                pickedUpObjRB = null;
                pickedUpObj = null;
                EnableAction();
            }
        }
    }


    public void PlaceObj()
    {
        if (pickedUpObj != null)
        {
            pickedUpObjRB.useGravity = false;
            pickedUpObjRB.constraints = RigidbodyConstraints.None;
            pickedUpObjRB.isKinematic = true;
            pickedUpObjRB = null;
            pickedUpObj = null;
            EnableAction();
        }
    }


    void DisableAction()
    {
        _playerMovement.FindAction("Move").Disable();
    }
    void EnableAction()
    {
        _playerMovement.FindAction("Move").Enable();
    }
}
