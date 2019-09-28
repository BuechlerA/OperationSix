using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField]
    private Camera cam;

    private float currentSpeed;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    public float gravity = 20f;

    public bool isRunning = false;

    public void GetInput(Vector3 moveVector, Vector3 viewVector)
    {
        PlayerMove(moveVector);
        PlayerView(viewVector);
    }

    public void PlayerMove(Vector3 moveVector)
    {
        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        characterController.Move(transform.TransformDirection(moveVector.x, 0, moveVector.z) * currentSpeed * Time.deltaTime);
    }

    public void PlayerView(Vector3 viewVector)
    {
        transform.localEulerAngles += new Vector3(0, viewVector.y, 0);
        cam.transform.localEulerAngles += new Vector3(Mathf.Clamp(-viewVector.x, -80f, 80f), 0, 0);
    }
}
