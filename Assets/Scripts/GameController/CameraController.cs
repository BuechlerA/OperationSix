using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float panSpeed = 20f;
    float zoomSpeed = 2f;
    public float rotateSpeed = 5f;

    float[] BoundaryX = new float[] { -100f, 100f };
    float[] BoundaryZ = new float[] { -100f, 100f };

    float[] zoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId;

    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        HandleMouse();
        PinchZoom();
        RotateCamera();
#endif
#if UNITY_ANDROID
        HandleTouch();
        //PinchZoom();
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
        switch(Input.touchCount) {
    
                case 1: // Panning
                    wasZoomingLastFrame = false;
            
                    // If the touch began, capture its position and its finger ID.
                    // Otherwise, if the finger ID of the touch doesn't match, skip it.
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began) {
                        lastPanPosition = touch.position;
                        panFingerId = touch.fingerId;
                    } else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
                        PanCamera(touch.position);
                    }
                    break;
    
                case 2: // Zooming
                    Vector2[] newPositions = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
                    if (!wasZoomingLastFrame) {
                        lastZoomPositions = newPositions;
                        wasZoomingLastFrame = true;
                    } else {
                        // Zoom based on the distance between the new positions compared to the 
                        // distance between the previous positions.
                        float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                        float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                        float offset = newDistance - oldDistance;
    
                        ZoomCamera(offset, zoomSpeed);
    
                        lastZoomPositions = newPositions;
                    }
                    break;
            
                default: 
                    wasZoomingLastFrame = false;
                    break;
                }
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

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


    void PinchZoom()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (cam.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
            }
        }
    }

    void RotateCamera()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(hit.point, Time.deltaTime * rotateSpeed);
            }
        }

    }
}
