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

    public float placementSpeed = 0.5f;
    public float placementRotationSpeed = 0.5f;

    RaycastHit hit;

    Transform objToMoveTowards;

    private void Start()
    {
        telekinesis = GameObject.FindGameObjectWithTag("Player").GetComponent<Telekinesis>();
    }

    void Update()
    {
        CheckForCorrectObj();
        MoveBlocksTowards();
    }



    public void CheckForCorrectObj()
    {
        if (grabbable == true && Physics.Raycast(transform.position, transform.transform.TransformDirection(Vector3.down), out hit, 1f, BuildingBlockLayer))
        {
            if (hit.transform.GetComponent<IsShapeAllowed>().allowedMatch.myMatch == iCanMatchWith.myMatch)
            {
                shouldMoveTowards = true;
                grabbable = false;
                telekinesis.PlaceObj();
                objToMoveTowards = hit.transform;
            }
            else
                Debug.Log("WRONG OBJ U FUCKING IDIOT");
        }
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(Vector3.down) * 1, Color.red);
    }

    public void MoveBlocksTowards()
    {
        if (shouldMoveTowards)
        {
            //Movetowards position
            float step = placementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.position, step);

            //rotate towards position
            float singleStep = placementRotationSpeed * Time.deltaTime;
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, objToMoveTowards.TransformDirection(Vector3.forward), singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            //if (transform.position == objToMoveTowards.position && transform.rotation == objToMoveTowards.rotation)
            if (Vector3.Distance(transform.position, objToMoveTowards.position) <= .1f && Quaternion.Angle(objToMoveTowards.rotation, Quaternion.identity) < 2f)
            {
                shouldMoveTowards = false;
                objToMoveTowards.GetComponent<MeshRenderer>().enabled = true;
                objToMoveTowards.GetComponent<Collider>().isTrigger = false;
                objToMoveTowards.GetComponent<EnableNextObject>().EnableObject();
                Destroy(gameObject);
                if (myCounter != null)
                    myCounter.value += 1;
            }
        }
    }
}
