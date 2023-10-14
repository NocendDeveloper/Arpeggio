using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class CameraDropdown : MonoBehaviourDpm
{
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
    }

    public void ChangeCamera(int selected)
    {
        string camera = "";
        
        switch (selected)
        {
            case 0:
                camera = ConstantResources.Configuration.Cameras.Orthographic;
                break;
            case 1:
                camera = ConstantResources.Configuration.Cameras.Perspective;
                break;
        }
        
        PlayerPrefs.SetString(ConstantResources.Configuration.Cameras.PrefString, camera);
        
        DpmLogger.Log("Camera changed to: " + camera);
    }
}
