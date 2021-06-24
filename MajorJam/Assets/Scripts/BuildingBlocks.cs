using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocks : MonoBehaviour
{
    public IntSO myCounter;
    public LayerMask targetLayer;
    private Telekinesis telekinesis;
    bool shouldMoveTowards = false;
    public bool grabbable = true;

    public float placementSpeed = 0.5f;
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
        if (grabbable == true && Physics.Raycast(transform.position, transform.transform.TransformDirection(Vector3.down), out hit, 1f, targetLayer))
        {
            shouldMoveTowards = true;
            grabbable = false;
            telekinesis.PlaceObj();
            objToMoveTowards = hit.transform;
        }
        Debug.DrawRay(transform.position, transform.transform.TransformDirection(Vector3.down) * 1, Color.red);
    }

    public void MoveBlocksTowards()
    {
        if (shouldMoveTowards)
        {
            float step = placementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, objToMoveTowards.position, step);

            float singleStep = placementSpeed * Time.deltaTime;
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, objToMoveTowards.TransformDirection(Vector3.forward), singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (transform.position == objToMoveTowards.position && transform.rotation == objToMoveTowards.rotation)
            {
                shouldMoveTowards = false;
                objToMoveTowards.GetComponent<MeshRenderer>().enabled = true;
                objToMoveTowards.GetComponent<Collider>().isTrigger = false;
                gameObject.SetActive(false);
                myCounter.value += 1;
            }
        }
    }
}
