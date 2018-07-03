using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaypointController : MonoBehaviour
{
    private bool isDragging = false;


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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, groundLayer))
            {
                myTouch = hit.point;
            }

            squadController.MoveToWaypoint(myTouch);
            wayPointGizmo.transform.position = myTouch;
        }
#endif

        #region AndroidSpecific
#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {

                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(touchRay, out hit, groundLayer))
                {
                    myTouch = hit.point;
                }

            }
        }

        squadController.MoveToWaypoint(myTouch);
        wayPointGizmo.transform.position = myTouch;
    }
#endif

        #endregion
}
