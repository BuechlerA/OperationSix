using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OperatorAI : MonoBehaviour
{

    public List<Transform> currentTargets;
    public GunBase currentGun;

    [SerializeField]
    [Range(0, 100)]
    private float damping = 2.0f;

    private bool detectedEnemy = false;

    [SerializeField]
    private NavMeshAgent soldierNavMeshAgent;
    private Animation_Soldier animationSoldier;

    private void Start()
    {
        currentTargets = GetComponent<FieldOfView>().visibleTargets;
        currentGun = GetComponentInChildren<GunBase>();
        soldierNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        LookAtEnemy();
        AttackEnemy();

        if (true)
        {

        }
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

                detectedEnemy = true;
            }

        }
    }

    private void AttackEnemy()
    {
        if (!detectedEnemy)
        {
            return;
        }       
        else if (currentTargets[0] != null && !currentGun.isEmpty)
        {
            currentGun.ShootGun();

            animationSoldier.SetShooting();
        }
        else if (currentGun.isEmpty)
        {
            currentGun.ReloadGun();
            Debug.Log("reloading");
        }
    }

    //bool IsEnemyAlive()
    //{
    //    if (currentTargets[0] != null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
