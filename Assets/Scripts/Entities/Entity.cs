using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float health = 3f;
    private float mentality = 3f;
    public StanceType stance;

    public bool isDead;

    public virtual void Move()
    {
#if UNITY_STANDALONE
        if(Input.GetMouseButtonDown(0))
        {
            //Do stuff here
        }
#endif

#if UNITY_ANDROID
    
#endif


    }

    public virtual void Follow()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Retreat()
    {

    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        mentality -= damage;

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    protected void Die()
    {
        isDead = true;
        //Destroy(gameObject);
        GetComponentInChildren<Animator>().enabled = false;
        if (GetComponentInChildren<NavMeshAgent>() != null)
        {
            GetComponentInChildren<NavMeshAgent>().enabled = false;
        }
        if (GetComponentInChildren<ViewVisualization>() != null)
        {
            GetComponentInChildren<ViewVisualization>().Disable();
        }
        GetComponent<Collider>().enabled = false;
        
        gameObject.layer = 13;

        if (gameObject.tag == "Player")
        {
            Debug.Log("Player died");
        }
    }
}
