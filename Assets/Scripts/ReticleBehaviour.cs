using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleBehaviour : MonoBehaviour
{
    private RectTransform reticle;
    public PlayerBehaviour player;

    public float restingSize;
    public float maxSize;
    public float speed;
    private float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (player.isRunning)
        {
            maxSize = 300f;
        }
        else
        {
            maxSize = 200f;
        }

        if (isMoving && !isLooking)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize -50, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        if (isLooking && !isMoving)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize - 100, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        if (isMoving && isLooking)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }



    bool isMoving
    {
        get
        {

            if (
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetAxis("Fire1") != 0
                    )
                return true;
            else
                return false;

        }
    }

    bool isLooking
    {
        get
        {

            if (

                Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0             
                    )
                return true;
            else
                return false;

        }
    }

    bool isShooting
    {
        get
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
