using System.Collections;
using System.Collections.Generic;
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
        DpmLogger.Log("Change camera to: " + selected);
    }
}
