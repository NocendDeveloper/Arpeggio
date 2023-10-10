using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : NoteCore
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes")) NoteFailed();
        if (other.gameObject.CompareTag("Fretter")) NoteFretted();
    }

    private void NoteFailed()
    {
        gameObject.SetActive(false);
        ScoreController.Instance.ScoreControl(ScoreController.Actions.RESET_STREAK);
    }
    
    private void NoteFretted()
    {
        gameObject.SetActive(false);
        ScoreController.Instance.ScoreControl(ScoreController.Actions.UP);
    }
}
