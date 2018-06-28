using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public SquadName squadName;
    public SquadSize squadSize;

    public bool isWaitingGOCODE;

    public OperatorBase[] operatorList;

    [SerializeField]
    private Transform[] rallyPoints;

    [SerializeField]
    private Transform currentWaypoint;

    public void Update()
    {
        MoveToWaypoint();
    }

    public void SquadSelection()
    {
        for (int i = 0; i < operatorList.Length; i++)
        {
            operatorList[i].OnSelect();
        }
    }

    public void MoveToWaypoint()
    {
        for (int i = 0; i < operatorList.Length; i++)
        {
            operatorList[i].GetComponent<NavMeshAgent>().SetDestination(rallyPoints[i].position);
        }
    }
}
