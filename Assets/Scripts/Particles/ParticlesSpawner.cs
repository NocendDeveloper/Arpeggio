using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{

    public ParticleSystem ParticleSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ParticleSystem.Play();
    }
}
