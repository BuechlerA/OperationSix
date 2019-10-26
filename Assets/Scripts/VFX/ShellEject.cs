using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEject : FadeEffect
{

    [SerializeField]
    private Rigidbody shellRigidbody;

    [SerializeField]
    private float forceMin;
    [SerializeField]
    private float forceMax;

    private void Start()
    {
        float force = Random.Range(forceMin, forceMax);
        shellRigidbody.AddForce(transform.forward * force);
        shellRigidbody.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine(Fade());
    }
}
