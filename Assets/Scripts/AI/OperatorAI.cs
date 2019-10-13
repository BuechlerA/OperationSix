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

    [SerializeField]
    private float agentVelocity = 1.0f;
    [SerializeField]
    private float currentAgentVelocity;

    [SerializeField]
    private bool detectedEnemy = false;

    //[SerializeField]
    private NavMeshAgent soldierNavMeshAgent;
    //[SerializeField]
    private Animation_Soldier animationSoldier;
    //[SerializeField]
    private GUIMessageText guiMessageText;

    private void Start()
    {
        currentTargets = GetComponent<FieldOfView>().visibleTargets;
        currentGun = GetComponentInChildren<GunBase>();
        soldierNavMeshAgent = GetComponent<NavMeshAgent>();
        animationSoldier = GetComponentInChildren<Animation_Soldier>();
        if (guiMessageText != null)
        {
            guiMessageText = GameObject.Find("MessageText").GetComponent<GUIMessageText>();
        }
    }

    private void LateUpdate()
    {
        if(!GetComponent<Entity>().isDead)
        {
            LookAtEnemy();
            AttackEnemy();

            currentAgentVelocity = soldierNavMeshAgent.velocity.magnitude;

            if (soldierNavMeshAgent.velocity.magnitude >= agentVelocity && animationSoldier != null)
            {
                animationSoldier.SetWalking();
                animationSoldier.SetWalkingSpeed(currentAgentVelocity);
            }
            else
            {
                animationSoldier.SetIdle();
                animationSoldier.SetWalkingSpeed(currentAgentVelocity);
            }
        }
        else
        {
            return;
        }
    }

    private void LookAtEnemy()
    {
        if (currentTargets.Count < 1)
        {
            detectedEnemy = false;
            return;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(currentTargets[0].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

            detectedEnemy = true;
            if (gameObject.tag == "friendly")
            {
                guiMessageText.SetText("Enemy Found!");
            }
        }
    }

    private void AttackEnemy()
    {
        if (!detectedEnemy)
        {           
            return;
        }
        else if (currentTargets[0].gameObject.GetComponent<Entity>().isDead)
        {
            guiMessageText.SetText("Enemy neutralized!");
        }
        else if (currentTargets[0] != null && !currentGun.isEmpty)
        {
            currentGun.ShootGun();

            animationSoldier.SetShooting();
        }
        else if (currentGun.isEmpty)
        {
            currentGun.ReloadGun();
            guiMessageText.SetText("Reloading!");
        }
    }

    //public void OpenDoor(GameObject door)
    //{
    //    soldierNavMeshAgent.SetDestination(door.Find("PlantPosition1").transform.position);
    //}

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
