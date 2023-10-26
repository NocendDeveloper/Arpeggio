using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraDropdown : MonoBehaviourDpm
{
    public TMP_Dropdown dropdown;
    
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        
        CheckConfig();
        SetInitialDropDownValue();
    }

    public void ChangeCamera(int selected)
    {
        string cameraConfig = "";
        
        switch (selected)
        {
            case 0:
                cameraConfig = ConstantResources.Configuration.Cameras.Orthographic;
                break;
            case 1:
                cameraConfig = ConstantResources.Configuration.Cameras.Perspective;
                break;
        }
        
        PlayerPrefs.SetString(ConstantResources.Configuration.Cameras.PrefString, cameraConfig);
        
        DpmLogger.Log("Camera changed to: " + cameraConfig);
    }

    private void CheckConfig()
    {
        if (PlayerPrefs.GetString(ConstantResources.Configuration.Cameras.PrefString, "").Equals(""))
        {
            ChangeCamera(0);
        }
    }

    private void SetInitialDropDownValue()
    {
        switch (PlayerPrefs.GetString(ConstantResources.Configuration.Cameras.PrefString))
        {
            case ConstantResources.Configuration.Cameras.Orthographic:
                dropdown.value = 0;
                break;
            case ConstantResources.Configuration.Cameras.Perspective:
                dropdown.value = 1;
                break;
        }
    }
}
