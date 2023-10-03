using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Fretter : MonoBehaviourDpm
{
    public new Collider collider;
    public float unFretAt;
    private float _timer;

    public InputAction inputAction;

    public Color color;

    private Material _material;
    
    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        _material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        inputAction.Enable(); 
    }

    private void OnDisable()
    {
        inputAction.Disable(); 
    }

    private void Update()
    {
        FretControl(inputAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note")) UnFret();
    }

    /**
     * This method controls that the player dont hold de button for take all notes.
     */
    private void FretControl(InputAction inputAction)
    {
        if (collider.enabled) _timer += Time.deltaTime * 1f;
        
        if (_timer > unFretAt) UnFret();
        else if (inputAction.WasPressedThisFrame()) Fret();
        
        if (inputAction.WasReleasedThisFrame()) UnFret();
    }

    private void Fret()
    {
        collider.enabled = true;
        _material.SetColor(1, Color.black);
    }

    private void UnFret()
    {
        collider.enabled = false;
        _material.SetColor(1, Color.blue);
        _timer = 0;
    }
}
