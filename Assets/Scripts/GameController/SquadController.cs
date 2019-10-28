using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public SquadName squadName;
    public SquadSize squadSize;

    public bool isWaitingGOCODE;
    public bool isMovingToDoor;
    public bool isCurrentlyOpening = false;

    public OperatorBase[] operatorList;

    [SerializeField]
    private Transform[] rallyPoints;

    [SerializeField]
    private Transform currentWaypoint;

    private DoorBehaviour currentDoor;


    public void Update()
    {
        //MoveWithPlayer();
        //Debug.Log("Remaining Distance to Spot:" + operatorList[0].GetComponent<NavMeshAgent>().remainingDistance);

        if (isMovingToDoor)
        {
            for (int i = 0; i < operatorList.Length; i++)
            {
                if (operatorList[i].GetComponent<NavMeshAgent>().remainingDistance <= 0.1f)
                {
                    Debug.Log("arrived at Door!");
                    if (!isCurrentlyOpening)
                    {
                        StartCoroutine(OpenLock());
                    }
                    //currentDoor.SetLock(true);

                    isMovingToDoor = false;
                }
            }
        }

        MoveToWaypoint(transform.position);
    }

    public void SquadSelection()
    {
        for (int i = 0; i < operatorList.Length; i++)
        {
            operatorList[i].OnSelect();
        }
    }

    public void MoveToWaypoint(Vector3 myTouch)
    {
        //Debug.Log("current waypoint: " + myTouch);
        transform.position = myTouch;

            for (int i = 0; i < operatorList.Length; i++)
            {
                if (!operatorList[i].isDead)
                {
                    //operatorList[i].GetComponent<NavMeshAgent>().SetDestination(myTouch);
                    operatorList[i].GetComponent<NavMeshAgent>().SetDestination(rallyPoints[i].position);
                }
                else
                {
                    return;
                }
            }
    }

    public void MoveToDoor(Vector3 myTouch, Transform[] doorPoints, DoorBehaviour selectedDoor)
    {
        isMovingToDoor = true;

        currentDoor = selectedDoor;

        for (int i = 0; i < operatorList.Length; i++)
        {
            //operatorList[i].GetComponent<NavMeshAgent>().SetDestination(myTouch);
            operatorList[i].GetComponent<NavMeshAgent>().SetDestination(doorPoints[i].position);
        }
    }

    IEnumerator OpenLock()
    {
        isCurrentlyOpening = true;
        Debug.Log("unlocking door!");
        yield return new WaitForSeconds(3f);
        isCurrentlyOpening = false;
    }

    void MoveWithPlayer()
    {
        Transform playerPos = GameObject.Find("Player").transform;

        transform.position = Vector3.Lerp(transform.position, Vector3.Scale(playerPos.position,new Vector3(0.5f, 0, 0.5f)), Time.deltaTime);
    }
}
