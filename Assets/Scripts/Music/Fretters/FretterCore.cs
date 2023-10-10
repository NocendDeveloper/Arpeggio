using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class FretterCore : MonoBehaviourDpm
{
    protected new Collider collider;
    protected float timer;
    public float unFretAt;

    public InputAction inputAction;

    public Material[] colors;

    protected new Renderer renderer;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        renderer = gameObject.GetComponent<Renderer>();
        collider = gameObject.GetComponent<Collider>();
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

    protected abstract void FretControl(InputAction inputAction);

}
