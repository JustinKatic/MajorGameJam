using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class AllPuzzlesComplete : MonoBehaviour
{
    UnityEvent PuzzlesCompleted;

    List<ObjectFits> _checkAllPuzzlesComplete;

    // Start is called before the first frame update
    void Start()
    {
        _checkAllPuzzlesComplete = new List<ObjectFits>();

        _checkAllPuzzlesComplete = GameObject.FindObjectsOfType<ObjectFits>().ToList();

    }


    public void CheckAllPuzzlesComplete()
    {
        for (int i = 0; i < _checkAllPuzzlesComplete.Count; i++)
        {
            if (i == _checkAllPuzzlesComplete.Count)
            {
                if (PuzzlesCompleted != null)
                    PuzzlesCompleted.Invoke();
                //Do Something
            }

            if (!_checkAllPuzzlesComplete[i]._objectFits)
            {
                break;
            }
        }
    }

}
