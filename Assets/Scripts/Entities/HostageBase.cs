using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostageBase : Entity
{

    private Transform escortingOperator;
    private NavMeshAgent myNavAgent;

    private void Start()
    {
        myNavAgent = GetComponent<NavMeshAgent>();
    }

    public override void Follow()
    {
        //base.Follow();
        myNavAgent.SetDestination(escortingOperator.transform.position);
    }
}
