using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectFits : MonoBehaviour
{
    Animator _animator;
    [SerializeField] GameObject _puzzlePiece;

    public float _tolerance = 20f;
    Vector3 _fitRotation;
    public bool _objectFits = false;

    public UnityEvent PuzzlesComplete;
    AllPuzzlesComplete _puzzleCheck;

    // Start is called before the first frame update
    void Start()
    {
        _puzzleCheck = FindObjectOfType<AllPuzzlesComplete>();

        if (_puzzleCheck != null)
        {
            PuzzlesComplete.AddListener(_puzzleCheck.CheckAllPuzzlesComplete);
        }

        if (_puzzlePiece != null)
        {
            _animator = _puzzlePiece.GetComponent<Animator>();

        }
        else
        {
            Debug.Log($"PuzzlePiece not found" + this.name.ToString()) ;
        }

        _fitRotation = transform.eulerAngles;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject == _puzzlePiece)
        {
            if (CheckRotation(_puzzlePiece.transform))
            {
                if (PuzzlesComplete != null)
                    PuzzlesComplete.Invoke();

                //Play Animation
                
            }
        }
    }

    public bool CheckRotation(Transform obj)
    {
        if (Mathf.Abs(_fitRotation.x - obj.eulerAngles.x) < _tolerance  && Mathf.Abs(_fitRotation.y - obj.eulerAngles.y) < _tolerance && Mathf.Abs(_fitRotation.z - obj.eulerAngles.z) < _tolerance)
        {

            _objectFits = true;
            Debug.Log(_objectFits);

            return true;
        }
        if (Quaternion.Angle(obj.rotation, Quaternion.identity) > 45f)
        {
            _objectFits = false;
            Debug.Log(_objectFits);

            return false;
        }
        return false;
    }

}
