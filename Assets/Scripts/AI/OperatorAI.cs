using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorAI : MonoBehaviour
{

    public List<Transform> currentTargets;

    [SerializeField]
    [Range(0, 100)]
    private float damping = 2.0f;


    private void Start()
    {
        currentTargets = GetComponent<FieldOfView>().visibleTargets;
    }

    private void LateUpdate()
    {
        LookAtEnemy();
    }

    private void LookAtEnemy()
    {
        if (currentTargets.Count >= 1)
        {
            if (currentTargets[0] == null)
            {
                return;               
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(currentTargets[0].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }

        }
    }
}
