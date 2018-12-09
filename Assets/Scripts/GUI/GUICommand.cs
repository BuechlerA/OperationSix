using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICommand : MonoBehaviour
{
    [SerializeField]
    private Vector3 clickPosition;
    private Vector3 doorPosition;

    public LayerMask doorLayer;

    private WaypointController waypointController;

    private GameObject commandPanel;

    int childLength;
    [SerializeField]
    private List<Transform> currentChildren;

	void Start ()
    {
        commandPanel = GameObject.Find("CommandUI");
        waypointController = GameObject.Find("SquadController").GetComponent<WaypointController>();
        childLength = commandPanel.transform.childCount;
        
        for (int i = 0; i < childLength; i++)
        {
            Transform currentChild = commandPanel.transform.GetChild(i);
            currentChildren.Add(currentChild);
        }

        SetChildrenStatus(false);
	}

	void Update ()
    {
        if (Input.GetMouseButtonUp(1))
        {
            clickPosition = Input.mousePosition;
            SetPosition(clickPosition);
        }

	}

    void SetChildrenStatus(bool toSet)
    {
        for (int i = 0; i < currentChildren.Count; i++)
        {
            currentChildren[i].gameObject.SetActive(toSet);
        }
    }

    void SetPosition(Vector2 clickPosition)
    {
        SetChildrenStatus(true);

        gameObject.transform.position = clickPosition;
    }

    public void WalkButton()
    {
        waypointController.GoToPoint(clickPosition);
        ExitMenu();
    }

    public void OpenDoorButton()
    {
        waypointController.GoToDoor(clickPosition);
        ExitMenu();
    }

    public void ExitMenu()
    {
        SetChildrenStatus(false);
    }


}
