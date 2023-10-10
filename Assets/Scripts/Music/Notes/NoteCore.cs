using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCore : MonoBehaviourDpm
{
    public int note;
    public int octave;
    public long length;
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
        NoteGoDown();
    }
    
    private void NoteGoDown()
    {
        transform.Translate(Vector3.down * (velocity * Time.deltaTime));
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
