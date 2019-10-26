using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{
    [Range(-90f, 20f)]
    public float doorHinge = -90f;
    public GameObject hingeObject;
    public GameObject doorObject;

    public bool isLocked = false;

    public void SetLock(bool lockState)
    {
        if (isLocked != lockState)
        {
            isLocked = lockState;    
        }
    }

    [ContextMenu("SetDoor")]
    public void SetDoor(float amount)
    {
        hingeObject.transform.Rotate(0, 0, amount, Space.Self);
    }
    private void Update()
    {
        RealtimeDoorState();
    }

    private void RealtimeDoorState()
    {
        if (isLocked)
        {
            return;
        }
        else
        {
            hingeObject.transform.rotation = Quaternion.Euler(-90, 0, doorHinge);
            doorObject.transform.rotation = Quaternion.Euler(-90, 0, doorHinge);
        }
    }
}
