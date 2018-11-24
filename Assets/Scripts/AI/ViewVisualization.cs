using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVisualization : MonoBehaviour
{

    public GameObject viewCone;

	void Start ()
    {
        viewCone = gameObject;
	}

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
