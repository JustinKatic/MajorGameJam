using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    public IntSO myCounter;
    public int numberOfBlocksToComplete;
    public GameEvent myCompleteEvent;

    private void Start()
    {
        myCounter.value = 0;
    }
    private void Update()
    {
        if (myCounter.value >= numberOfBlocksToComplete)
        {
            myCompleteEvent.Raise();
            myCounter.value = 0;
        }
    }
}
