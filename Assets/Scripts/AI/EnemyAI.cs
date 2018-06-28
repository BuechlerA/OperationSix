using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> currentTargets;

    [SerializeField]
    [Range(0,100)]
    private float damping = 20f;

    private void Start()
    {
        currentTargets = GetComponent<FieldOfView>().visibleTargets;
    }

    private void Update()
    {
        if (currentTargets.Count > 0)
        {
            //transform.LookAt(currentTargets[0]);

            Quaternion rotation = Quaternion.LookRotation(currentTargets[0].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }
}
