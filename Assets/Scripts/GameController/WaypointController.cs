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

    public List<Vector3> waypoints = new List<Vector3>();

    int index = 0;

    void Start()
    {
        squadController = GetComponent<SquadController>();
    }

    void Update()
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

                //squadController.MoveToWaypoint(myTouch);
                wayPointGizmo.transform.position = myTouch;
                waypoints.Add(myTouch);
                GoToPoint(index);
                index++;
                //for (int i = 0; i < waypoints.Count; i++)
                //{
                //    squadController.MoveToWaypoint(waypoints[i]);
                //}
                isClickedEvent = false;
            }
        }
#endif
    }

    void GoToPoint(int i)
    {
        squadController.MoveToWaypoint(waypoints[i]);     
    }

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
}
