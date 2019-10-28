using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{

    public LayerMask entityLayer;
    public float detonationTime = 3f;
    public float detonationRange = 5f;

    public AudioClip[] audioClips = new AudioClip[3];
    private AudioSource audioSource;

    public GameObject explosionParticle;

    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();       
        StartCoroutine(Sequence(detonationTime));   
    }

    public virtual void Detonate()
    {
        Debug.Log("Exploded");       
        Collider[] hitsList = Physics.OverlapSphere(transform.position, detonationRange, entityLayer);
        foreach (Collider hitObj in hitsList)
        {
            Debug.Log(hitObj.name);

            Ray ray = new Ray(transform.position, hitObj.transform.position - transform.position);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, detonationRange, entityLayer))
            {
                Debug.DrawRay(transform.position, hitObj.transform.position - transform.position, Color.magenta, 1.5f);
                hitObj.GetComponent<Entity>().TakeHit(5f, hit);
            }
        }

        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual IEnumerator Sequence(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(audioClips[Random.Range(0, 2)]);
        Detonate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detonationRange);
    }
}
