using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image greenCircle;
    [SerializeField]
    private Image outerCircle;

    float fillSpeed = 5f;

    bool isFillingActive;

	void Start ()
    {
        SetImages(false);
        greenCircle.fillAmount = 0f;
	}
	
	void Update ()
    {
        if (isFillingActive)
        {
            greenCircle.fillAmount += Time.deltaTime / fillSpeed;
        }
        if (greenCircle.fillAmount == 1f)
        {
            SetImages(false);
        }
        else
        {
            return;
        }
    }

    [ContextMenu("StartTask")]
    public void StartTask()
    {
        if (isFillingActive == false)
        {
            SetImages(true);
            isFillingActive = true;
        }
        else
        {
            return;
        }
    }

    void SetImages(bool status)
    {
        greenCircle.gameObject.SetActive(status);
        outerCircle.gameObject.SetActive(status);
    }

}
