using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{

    private float health;
    private float mentality;

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

    public void TakeHit()
    {

    }
}
