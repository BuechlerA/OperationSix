using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;

    private Vector3 moveInput;
    private Vector3 viewInput;

    private bool inputEnabled = true;

    public void Start()
    {
        if (playerBehaviour != null)
        {
            playerBehaviour.GetComponent<PlayerBehaviour>();
        }

        EnableInput();
    }

    public void Update()
    {
        if (inputEnabled)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            float horizontalView = Input.GetAxisRaw("Mouse Y");
            float verticalView = Input.GetAxisRaw("Mouse X");

            moveInput = new Vector3(horizontalMove, 0, verticalMove);
            viewInput = new Vector3(horizontalView, verticalView, 0);

            if (inputEnabled && playerBehaviour != null)
            {
                playerBehaviour.GetInput(moveInput, viewInput);
            }

            if (Input.GetButton("Sprint"))
            {
                playerBehaviour.isRunning = true;
            }
            else
            {
                playerBehaviour.isRunning = false;
            }
        }
    }

    public void EnableInput()
    {
        inputEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
