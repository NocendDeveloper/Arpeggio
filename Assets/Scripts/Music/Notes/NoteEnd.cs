using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEnd : NoteCore
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes")) gameObject.SetActive(false);
    }
}
