using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Range(0, 1)]
    public float timeFactor;

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        Time.timeScale = timeFactor;
    }
}
