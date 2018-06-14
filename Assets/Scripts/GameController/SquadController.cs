using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public SquadName squadName;
    public SquadSize squadSize;

    public bool isWaitingGOCODE;

    public List<OperatorBase> operatorList;

    public void Start()
    {

        for (int i = 0; i < (int)squadSize; i++)
        {
            Debug.Log("SquadPosition: " + i);
        }
    }
}
