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

    private float placementSpeed = 3f;
    private float placementRotationSpeed = 3f;

    public bool rayCastDown;

    RaycastHit hit;

    Transform objToMoveTowards;

    Vector3 raycastDir;

    Vector3 startPos;
    Quaternion startRot;

    DebrisTrigger debrisTrigger;

    AudioSource audioSource;
    public AudioClip woodSlottingInHoleSound;
    public AudioClip hitWallSound;
    public AudioClip wrongObjectSound;
    bool initialPositionReached = false;



    private void Start()
    {
        telekinesis = GameObject.FindGameObjectWithTag("Player").GetComponent<Telekinesis>();
        startPos = transform.position;
        startRot = transform.rotation;
        debrisTrigger = GameObject.FindGameObjectWithTag("Debris").GetComponent<DebrisTrigger>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (telekinesis.pickedUpObj)
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
            audioSource.PlayOneShot(hitWallSound);

        }
    }


    public void CheckForCorrectObj()
    {
        if (rayCastDown)
            raycastDir = Vector3.down;
        else
            raycastDir = Vector3.up;


        if (grabbable == true && Physics.Raycast(transform.position, transform.transform.TransformDirection(raycastDir), out hit, 2.5f, BuildingBlockLayer))
        {
            if (hit.transform.GetComponent<IsShapeAllowed>() != null && hit.transform.GetComponent<IsShapeAllowed>().allowedMatch.myMatch == iCanMatchWith.myMatch)
            {
                transform.GetComponent<Collider>().enabled = false;
                shouldMoveTowards = true;
                grabbable = false;
                telekinesis.PlaceObj();
                objToMoveTowards = hit.transform;
                audioSource.PlayOneShot(woodSlottingInHoleSound);
            }
            else
            {
                Rigidbody tempObj = telekinesis.pickedUpObjRB;
                telekinesis.DropObj();
                audioSource.PlayOneShot(wrongObjectSound);
                if (tempObj != null)
                    tempObj.AddForce(-hit.transform.position, ForceMode.Impulse);
            }
        }
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(raycastDir) * 2.5f, Color.red);
    }

    public void MoveBlocksTowards()
    {
        //insert comments here justin
        if (shouldMoveTowards)
        {
            float step = placementSpeed * Time.deltaTime;
            if (!initialPositionReached)
                transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.GetChild(0).transform.position, step);
            if (Vector3.Distance(transform.position, objToMoveTowards.GetChild(0).transform.position) <= 0.05f)
            {
                initialPositionReached = true;
            }
            if (initialPositionReached)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, objToMoveTowards.rotation, placementRotationSpeed * Time.deltaTime);

                float angle = Quaternion.Angle(transform.rotation, objToMoveTowards.rotation);
                if (angle < 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.position, step);
                }

                //if (transform.position == objToMoveTowards.position && transform.rotation == objToMoveTowards.rotation)
                if (Vector3.Distance(transform.position, objToMoveTowards.position) <= 0.05f && angle < 0.1f)
                {
                    shouldMoveTowards = false;
                    //objToMoveTowards.GetComponent<MeshRenderer>().enabled = false;
                    objToMoveTowards.GetComponent<EnableNextObject>().EnableObject();

                    Destroy(objToMoveTowards.gameObject);

                    debrisTrigger.PlayFX();

                    //Destroy(gameObject);
                    if (myCounter != null)
                        myCounter.value += 1;
                }
            }
        }
    }
}
