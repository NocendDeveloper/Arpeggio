using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSheetDropdown : MonoBehaviourDpm
{
    public TMP_Dropdown dropdown;
    
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        
        CheckConfig();
        SetInitialDropDownValue();
    }

    public void ChangeMusicSheet(int selected)
    {
        string sheetConfig = "";
        
        switch (selected)
        {
            case 0:
                sheetConfig = ConstantResources.Configuration.MusicSheet.Original;
                break;
            case 1:
                sheetConfig = ConstantResources.Configuration.MusicSheet.Matias;
                break;
        }
        
        PlayerPrefs.SetString(ConstantResources.Configuration.MusicSheet.PrefString, sheetConfig);
        
        DpmLogger.Log("Camera changed to: " + sheetConfig);
    }
    
    private void CheckConfig()
    {
        if (PlayerPrefs.GetString(ConstantResources.Configuration.MusicSheet.PrefString, "").Equals(""))
        {
            ChangeMusicSheet(0);
        }
    }

    private void SetInitialDropDownValue()
    {
        switch (PlayerPrefs.GetString(ConstantResources.Configuration.MusicSheet.PrefString))
        {
            case ConstantResources.Configuration.MusicSheet.Original:
                dropdown.value = 0;
                break;
            case ConstantResources.Configuration.MusicSheet.Matias:
                dropdown.value = 1;
                break;
        }
    }
}
