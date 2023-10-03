using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviourDpm
{
    public int velocity = 0;
    
    private Material _materialNote;

    public Material[] colors;

    private void Awake()
    {
        SetLogger(name, "#8B8BAE");
        _materialNote = GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.down * (velocity * Time.deltaTime));
        
        switch (gameObject.transform.position.x)
        {
            case -3:
                _materialNote = colors[0];
                break;
            case -1.5f:
                _materialNote = colors[1];
                break;
            case -0:
                _materialNote = colors[2];
                break;
            case 1.5f:
                _materialNote = colors[3];
                break;
            case 3:
                _materialNote = colors[4];
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
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
