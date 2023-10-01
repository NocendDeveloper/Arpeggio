using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviour
{
    [HideInInspector] public int code = 999;
    public int velocity = 0;
    [HideInInspector] public int duration = 0;
    [HideInInspector] public bool on;

    void Update()
    {
        transform.Translate(Vector2.down * (velocity * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes"))
        {
            Destroy(gameObject);
        }
    }
}
