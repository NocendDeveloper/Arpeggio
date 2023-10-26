using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CameraController : MonoBehaviourDpm
{
    public Camera perspectiveCamera;
    public Camera orthographicCamera;
    public MenuController menuController;

    private void Awake()
    {
        SetLogger(name, "#98DFEA");
    }

    private void OnEnable()
    {
        LoadCameraConfiguration();
    }

    private void LoadCameraConfiguration()
    {
        DpmLogger.Log("Loading camera configuration... ");
        string cameraPref = PlayerPrefs.GetString(ConstantResources.Configuration.Cameras.PrefString, "");

        if (cameraPref.Equals("")) return;
        
        perspectiveCamera.gameObject.SetActive(false);
        orthographicCamera.gameObject.SetActive(false);
        
        switch (cameraPref)
        {
            case ConstantResources.Configuration.Cameras.Perspective:
                perspectiveCamera.gameObject.SetActive(true);
                break;
            case ConstantResources.Configuration.Cameras.Orthographic:
                orthographicCamera.gameObject.SetActive(true);
                break;
        }
    }
}
