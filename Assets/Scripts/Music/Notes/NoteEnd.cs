using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class NoteEnd : NoteCore
{
    protected override void Awake()
    {
        base.Awake();
        _renderer.enabled = DebugMode.Instance.IsDebugModeEnabled();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes")) gameObject.SetActive(false);
    }
}
