using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Settings : MonoBehaviour
{
    public enum Prefs
    {
        CAMERA    
    }
    
    private static Settings _instance;
    
    public int cameraType;

    public void SaveSettings(Prefs pref, object value)
    {
        switch (pref)
        {
            case Prefs.CAMERA:
                PlayerPrefs.SetInt("cameraType", (int) value);
                break;
        }
    }
    
    public static void LoadSettings()
    {
        // cameraType = PlayerPrefs.GetInt("cameraType");
    }
    
    public static Settings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Settings>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<Settings>();
                }
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

}
