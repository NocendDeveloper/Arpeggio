using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviourDpm
{
    public int velocity = 0;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        SetLogger(name, "#8B8BAE");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.down * (velocity * Time.deltaTime));
        
        switch (gameObject.transform.position.x)
        {
            case -2:
                _spriteRenderer.color = Color.yellow;
                break;
            case -1:
                _spriteRenderer.color = Color.cyan;
                break;
            case -0:
                _spriteRenderer.color = Color.green;
                break;
            case 1:
                _spriteRenderer.color = Color.red;
                break;
            case 2:
                _spriteRenderer.color = Color.magenta;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Fretter"))
        {
            gameObject.SetActive(false);
        }
    }
}
