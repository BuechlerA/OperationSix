using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : FadeEffect
{
    public LayerMask collisionMask;

    float speed;
    float damage = 1f;

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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
            GameObject.Destroy(gameObject);
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
}
