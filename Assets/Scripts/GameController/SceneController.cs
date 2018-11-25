using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Range(0, 1)]
    public float timeFactor;

    private PauseController pauseController;

    private void Start()
    {
        pauseController = GetComponent<PauseController>();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        Time.timeScale = timeFactor;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseController.Pause();
        }
    }
}
