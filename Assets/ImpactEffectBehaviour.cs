using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectBehaviour : FadeEffect
{
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        PlayEffect();
    }

    void PlayEffect()
    {
        particle.Play();
        StartCoroutine(Fade());
    }



}
