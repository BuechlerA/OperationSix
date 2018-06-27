using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnDrawGizmos()
    {       
        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Vector3 currentWaypoint = waypoints[i].position;
            Gizmos.DrawSphere(currentWaypoint, .5f);
        }

    }
}
