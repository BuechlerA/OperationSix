using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField]
    private LayerMask entityLayer;
    [SerializeField]
    private LayerMask otherLayer;
    [SerializeField]
    private GameObject lastSelection;
    [SerializeField]
    private AudioController audioController;

    private void Awake()
    {
        audioController = GetComponent<AudioController>();
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, entityLayer) != lastSelection)
            {
                Debug.Log("selected player");
                hit.collider.GetComponent<OperatorBase>().OnSelect();

                if(lastSelection != null)
                {
                    lastSelection.GetComponent<OperatorBase>().OnUnselect();
                }
                else
                {
                    lastSelection = hit.collider.gameObject;
                }

                //Play Voice Clip for Selection
                PlayAudio();
            }
        }
#endif
    }

    public void PlayAudio()
    {
        audioController.AudioSelected();
    }
}
