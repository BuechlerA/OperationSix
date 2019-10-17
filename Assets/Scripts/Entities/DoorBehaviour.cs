using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{

    public bool doorState;
    [SerializeField]
    private Animator doorAnimator;

    public Transform[] doorRallyPoints = new Transform[3];

	void Start ()
    {   
        doorAnimator = GetComponentInChildren<Animator>();

        SetLock(doorState);
	}


    //false is closed
    //true is open
    [ContextMenu("SetLock")]
    public void SetLock(bool lockStatus)
    {
        doorState = lockStatus;

        doorAnimator.SetBool("isOpen", lockStatus);
        GetComponent<Collider>().enabled = !lockStatus;
        GetComponent<NavMeshObstacle>().enabled = !lockStatus;         
    }
}
