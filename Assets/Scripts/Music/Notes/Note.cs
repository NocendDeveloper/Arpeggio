using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviourDpm
{
    public int velocity = 0;

    private void Awake()
    {
        SetLogger(name, "#8B8BAE");
    }

    void Update()
    {
        transform.Translate(Vector2.down * (velocity * Time.deltaTime));
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
