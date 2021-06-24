using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class AllPuzzlesComplete : MonoBehaviour
{
    public UnityEvent PuzzlesCompleted;

    public List<ObjectFits> _checkAllPuzzlesComplete;

    // Start is called before the first frame update
    void Start()
    {
        _checkAllPuzzlesComplete = new List<ObjectFits>();

        if (_checkAllPuzzlesComplete.Count == 0)
        {
            _checkAllPuzzlesComplete = GameObject.FindObjectsOfType<ObjectFits>().ToList();

        }
        else
        {
            Debug.Log("No Puzzles Attached to" + gameObject.name);
        }

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
