using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{

    public bool isOpen;
    [SerializeField]
    private Animator doorAnimator;

    public Transform[] doorRallyPoints = new Transform[3];

	void Start ()
    {   
        doorAnimator = GetComponentInChildren<Animator>();

        SetLock(false);
	}


    //false is closed
    //true is open
    public void SetLock(bool lockStatus)
    {
        isOpen = lockStatus;

        doorAnimator.SetBool("isOpen", lockStatus);
        GetComponent<Collider>().enabled = !lockStatus;
        GetComponent<NavMeshObstacle>().enabled = !lockStatus;         
    }
}
