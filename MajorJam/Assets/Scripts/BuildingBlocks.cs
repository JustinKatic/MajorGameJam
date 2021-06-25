using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocks : MonoBehaviour
{
    public ShapeAndColor iCanMatchWith;
    public IntSO myCounter;
    public LayerMask BuildingBlockLayer;
    private Telekinesis telekinesis;
    bool shouldMoveTowards = false;
    public bool grabbable = true;

    private float placementSpeed = 1.5f;
    private float placementRotationSpeed = 3f;

    public bool rayCastDown;

    RaycastHit hit;

    Transform objToMoveTowards;

    Vector3 raycastDir;

    Vector3 startPos;
    Quaternion startRot;

    DebrisTrigger debrisTrigger;


    private void Start()
    {
        telekinesis = GameObject.FindGameObjectWithTag("Player").GetComponent<Telekinesis>();
        startPos = transform.position;
        startRot = transform.rotation;
        debrisTrigger = GameObject.FindGameObjectWithTag("Debris").GetComponent<DebrisTrigger>();
    }

    void Update()
    {
        CheckForCorrectObj();
        MoveBlocksTowards();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reset")
        {
            transform.position = startPos;
            transform.rotation = startRot;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("HIT WALL");
            telekinesis.DropObj();
        }
    }


    public void CheckForCorrectObj()
    {
        if (rayCastDown)
            raycastDir = Vector3.down;
        else
            raycastDir = Vector3.up;

        if (grabbable == true && Physics.Raycast(transform.position, transform.transform.TransformDirection(raycastDir), out hit, 3f, BuildingBlockLayer))
        {
            if (hit.transform.GetComponent<IsShapeAllowed>().allowedMatch.myMatch == iCanMatchWith.myMatch)
            {
                shouldMoveTowards = true;
                grabbable = false;
                telekinesis.PlaceObj();
                objToMoveTowards = hit.transform;
            }
            else
            {
                Rigidbody tempObj = telekinesis.pickedUpObjRB;
                telekinesis.DropObj();
                if (tempObj != null)
                    tempObj.AddForce(-hit.transform.position * 30);
            }

        }
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(raycastDir) * 1f, Color.red);
    }

    public void MoveBlocksTowards()
    {
        if (shouldMoveTowards)
        {
            transform.GetComponent<Collider>().enabled = false;

            transform.rotation = Quaternion.Lerp(transform.rotation, objToMoveTowards.rotation, placementRotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(objToMoveTowards.localRotation, Quaternion.identity) < .1f)
            {
                float step = placementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.position, step);
            }


            //if (transform.position == objToMoveTowards.position && transform.rotation == objToMoveTowards.rotation)
            if (Vector3.Distance(transform.position, objToMoveTowards.position) <= .1f)
            {
                shouldMoveTowards = false;
                //objToMoveTowards.GetComponent<MeshRenderer>().enabled = false;
                objToMoveTowards.GetComponent<EnableNextObject>().EnableObject();

                Destroy(objToMoveTowards.GetComponent<IsShapeAllowed>());

                debrisTrigger.PlayFX();


                //Destroy(gameObject);
                if (myCounter != null)
                    myCounter.value += 1;
            }
        }
    }
}
