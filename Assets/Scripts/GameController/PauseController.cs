using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    private SceneController sceneController;
    public bool isPaused = false;

    private void Start()
    {
        sceneController = GetComponent<SceneController>();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            sceneController.timeFactor = Mathf.Lerp(0f, 1f, Time.deltaTime);
            //Time.timeScale = 0;
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
            sceneController.timeFactor = Mathf.Lerp(1f, 0f, Time.deltaTime);
            isPaused = false;
        }
    }
}
