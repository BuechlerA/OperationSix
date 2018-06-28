using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    public bool isPaused = false;

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Continue();
        }
    }

    public void Continue()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
