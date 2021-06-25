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

    public bool rayCastDown;

    RaycastHit hit;

    Transform objToMoveTowards;

    Vector3 raycastDir;

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
        if (rayCastDown)
            raycastDir = Vector3.down;
        else
            raycastDir = Vector3.up;

        if (grabbable == true && Physics.Raycast(transform.position, transform.transform.TransformDirection(raycastDir), out hit, 1.5f, BuildingBlockLayer))
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
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(raycastDir) * 1, Color.red);
    }

    public void MoveBlocksTowards()
    {
        if (shouldMoveTowards)
        {
            //Movetowards position
            float step = placementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.position, step);

            transform.GetComponent<MeshCollider>().enabled = false;

            transform.rotation = Quaternion.Lerp(transform.rotation, objToMoveTowards.rotation, placementRotationSpeed * Time.deltaTime);

            //if (transform.position == objToMoveTowards.position && transform.rotation == objToMoveTowards.rotation)
            if (Vector3.Distance(transform.position, objToMoveTowards.position) <= .1f && Quaternion.Angle(objToMoveTowards.localRotation, Quaternion.identity) < 1f)
            {
                shouldMoveTowards = false;
                objToMoveTowards.GetComponent<MeshRenderer>().enabled = false;
                objToMoveTowards.GetComponent<EnableNextObject>().EnableObject();

                Destroy(objToMoveTowards.GetComponent<IsShapeAllowed>());
                
                
                Destroy(gameObject);
                if (myCounter != null)
                    myCounter.value += 1;
            }
        }
    }
}
