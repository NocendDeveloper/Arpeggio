using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSceneManager : MonoBehaviourDpm
{
    public CameraController cameraController;
    public MusicLoader musicLoader;
    public GameObject music;
    public GameObject fret;
    public ScoreScreen scoreScreen;

    private void Awake()
    {
        SetLogger(name, "#58BC82");

        DisableAll();
    }

    private void OnEnable()
    {
        scoreScreen.gameObject.SetActive(false);
        
        cameraController.gameObject.SetActive(true);
        music.SetActive(true);
    }

    private void DisableAll()
    {
        cameraController.gameObject.SetActive(false);
        music.SetActive(false);
        fret.SetActive(false);
    }
    
    private void Update()
    {
        if (musicLoader.midiLoaded && musicLoader.mp3Loaded && !fret.activeSelf)
        {
            fret.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
