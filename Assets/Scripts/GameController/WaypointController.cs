using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaypointController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isClickedEvent = false;

    private Vector3 myTouch;
    [SerializeField]
    private LayerMask groundLayer;

    private SquadController squadController;
    [SerializeField]
    private GameObject wayPointGizmo;

    public List<Transform> waypoints = new List<Transform>();

    void Start ()
    {
        squadController = GetComponent<SquadController>();
	}
	
	void Update ()
    {

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButtonUp(0))
        {
            isClickedEvent = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, groundLayer))
            {
                myTouch = hit.point;
            }

            if (isClickedEvent)
            {
                int index = 0;

                squadController.MoveToWaypoint(myTouch);
                wayPointGizmo.transform.position = myTouch;
                //waypoints.Add();
                index++;
                isClickedEvent = false;
            }
        }

#endif

        #region AndroidSpecific
#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                isClickedEvent = true;

                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(touchRay, out hit, groundLayer))
                {
                    myTouch = hit.point;
                }

            }
        }

        if (isClickedEvent)
        {
            squadController.MoveToWaypoint(myTouch);
            wayPointGizmo.transform.position = myTouch;
            isClickedEvent = false;
        }
    }
#endif

    #endregion

    private void OnDrawGizmos()
    {
        if (waypoints.Count > 0)
        {

            Gizmos.color = Color.green;

            foreach (Transform transform in waypoints)
            {
                Gizmos.DrawSphere(transform.position, 1f);
            }
        }
    }
}
