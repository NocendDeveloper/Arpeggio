using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FretterHold : FretterCore
{
    private void Start()
    {
        collider.enabled = false;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NoteStart")) StartParticles();
    }

    protected override void FretControl(InputAction inputAction)
    {
        if (inputAction.WasPressedThisFrame()) collider.enabled = true;
        if (inputAction.WasReleasedThisFrame()) collider.enabled = false;
    }

    private void StartParticles()
    {
        // TODO: IMPLEMENT
    }
}
