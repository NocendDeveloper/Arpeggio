using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fretter : MonoBehaviourDpm
{
    public Collider2D collider;
    public float unFretAt;
    private float _timer;

    public InputAction InputAction;

    private Color _color;
    private SpriteRenderer _spriteRenderer;

    public Color color;
    
    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        _color = gameObject.GetComponent<SpriteRenderer>().color;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InputAction.Enable(); 
    }

    private void OnDisable()
    {
        InputAction.Disable(); 
    }

    private void Update()
    {
        FretControl(InputAction);
    }

    private void OnTriggerEnter2D(Collider2D other)
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
        _color = color;
        _spriteRenderer.color = color;
    }

    private void UnFret()
    {
        collider.enabled = false;
        _spriteRenderer.color = Color.white;
        _timer = 0;
    }
}
