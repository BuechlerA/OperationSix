using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float panSpeed = 20f;
    float zoomSpeed = 2f;

    float[] BoundaryX = new float[] { -100f, 100f };
    float[] BoundaryZ = new float[] { -100f, 100f };

    float[] zoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId;

    private bool wasZooming;
    private Vector2[] lastZoomPosition;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        HandleMouse();
#endif
#if UNITY_ANDROID
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, zoomSpeed);
    }

    void HandleTouch()
    {

    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);

        //move cam
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundaryX[0], BoundaryX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundaryZ[0], BoundaryZ[1]);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        { return; }
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), zoomBounds[0], zoomBounds[1]);
    }
}
