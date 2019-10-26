using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : FadeEffect
{
    public LayerMask collisionMask;
    public GameObject[] impactEffects = new GameObject[2];
    public BulletholeBehaviour bulletHoleObject;

    float speed;
    float damage = 10f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;

        StartCoroutine(Fade());
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 3f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log(hit.collider.gameObject.GetType().ToString());
            OnHitObject(hit);
            if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "friendly" || hit.collider.gameObject.tag == "Player")
            {
                PlayImpactEffect(hit, ray, 1);
            }
            else
            {
                PlayImpactEffect(hit, ray, 0);
            }
            Destroy(gameObject);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        //Debug.Log(hit.collider.gameObject.name);
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }
    }

    void PlayImpactEffect(RaycastHit hit, Ray ray, int effectVariant)
    {
        //Generate Impact Spark effect
        Instantiate(impactEffects[effectVariant], hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

        //Generate BulletHole
        if (effectVariant == 0)
        {
            BulletholeBehaviour newBulletHole = Instantiate(bulletHoleObject, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal, Vector3.up));
            newBulletHole.SetSprite();
        }
    }
}
