using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStart : NoteCore
{
    public TrailRenderer trailRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes") && !other.gameObject.CompareTag("FretterHold")) gameObject.SetActive(false);
        if (other.gameObject.CompareTag("NoteEnd"))
        {
            if (Math.Abs(other.transform.position.y - transform.position.y) < 0.05) gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FretterHold"))
        {
            transform.Translate(Vector3.down * ((velocity * -1f) * Time.deltaTime));
            
            ScoreController.Instance.ScoreControl(ScoreController.Actions.UP_NO_STREAK);
        }
    }

    public void SetTrailColor()
    {
        switch (gameObject.transform.position.x)
        {
            case -3:
                trailRenderer.startColor = Color.yellow;
                break;
            case -1.5f:
                trailRenderer.startColor = Color.cyan;
                break;
            case -0:
                trailRenderer.startColor = Color.green;
                break;
            case 1.5f:
                trailRenderer.startColor = Color.red;
                break;
            case 3:
                trailRenderer.startColor = Color.magenta;
                break;
        }
    }
}
