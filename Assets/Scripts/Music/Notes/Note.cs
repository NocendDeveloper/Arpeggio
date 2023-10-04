using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Note : MonoBehaviourDpm
{
    public int velocity = 0;
    
    private Renderer _renderer;

    public Material[] colors;

    private void Awake()
    {
        SetLogger(name, "#8B8BAE");
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * (velocity * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyerNotes")) NoteFailed();
        if (other.gameObject.CompareTag("Fretter")) NoteFretted();
    }

    private void NoteFailed()
    {
        gameObject.SetActive(false);
        ScoreController.Instance.ResetStreak();
    }
    
    private void NoteFretted()
    {
        gameObject.SetActive(false);
        ScoreController.Instance.ScoreUp();
    }
    
    public void SetColor()
    {
        switch (gameObject.transform.position.x)
        {
            case -3:
                _renderer.material = colors[0];
                break;
            case -1.5f:
                _renderer.material = colors[1];
                break;
            case -0:
                _renderer.material = colors[2];
                break;
            case 1.5f:
                _renderer.material = colors[3];
                break;
            case 3:
                _renderer.material = colors[4];
                break;
        }
    }
}
