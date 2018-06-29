using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float health = 3f;
    private float mentality;
    public StanceType stance;

    protected bool isDead;

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

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
    }
}
