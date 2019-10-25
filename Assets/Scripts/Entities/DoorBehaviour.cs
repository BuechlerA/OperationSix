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

    [ContextMenu("SetDoor")]
    public void SetDoor(float amount)
    {
        hingeObject.transform.Rotate(0, 0, amount, Space.Self);
    }
    private void Update()
    {
        hingeObject.transform.rotation = Quaternion.Euler(-90, 0, doorHinge);
        doorObject.transform.rotation = Quaternion.Euler(-90, 0, doorHinge);
    }
}
