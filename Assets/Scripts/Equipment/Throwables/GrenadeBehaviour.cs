using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{

    public LayerMask entityLayer;
    public float detonationTime = 3f;

    public virtual void Start()
    {
        StartCoroutine(Sequence(detonationTime));
        
    }

    public virtual void Detonate()
    {
        Collider[] hitsList = Physics.OverlapSphere(transform.position, 5f, entityLayer);
        OnDrawGizmos();
    }

    public virtual IEnumerator Sequence(float time)
    {
        yield return new WaitForSeconds(time);
        Detonate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
