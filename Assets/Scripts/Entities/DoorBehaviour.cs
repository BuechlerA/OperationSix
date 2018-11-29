using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    public bool isOpen;

    [SerializeField]
    private Material doorMaterial;

    bool lockStatus = false;

	void Start ()
    {
        doorMaterial = GetComponent<Renderer>().material;
        SetLock(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void SetLock(bool lockStatus)
    {
        if (isOpen == lockStatus)
        {
            isOpen = !lockStatus;
            doorMaterial.color = Color.red;
        }
        else
        {
            doorMaterial.color = Color.green;
        }
    }
}
