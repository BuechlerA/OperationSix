using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMessageText : MonoBehaviour
{

    private Text myText;
	
	void Start ()
    {
        myText = GetComponent<Text>();
	}
	
    public void SetText(string message)
    {
        myText.text = message;
    }
}
