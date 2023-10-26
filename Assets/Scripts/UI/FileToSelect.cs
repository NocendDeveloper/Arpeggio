using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class FileToSelect : MonoBehaviourDpm
{
    /* BUTTON PROPERTIES */
    public TextMeshProUGUI songTitle;
    
    /* RECORDS */
    public ValueSetterInToText score;
    public ValueSetterInToText maxStreak;
    public ValueSetterInToText percentage;

    private void Awake()
    {
        SetLogger(name, "#208AAE");
    }

    private void Start()
    {
        SetRecords();
    }

    /**
     * Set de values of records in to the FileToSelect getting that value from PlayerPrefs (defaultValue: 0) and the
     * strings of PlayerPrefs from ConstantResources. Then call the SetValue method from ValueSetterInToText which needs
     * an array of object to work.
     */
    private void SetRecords()
    {
        DpmLogger.Log(songTitle.text);
        
        score.SetValue(new object[] { PlayerPrefs.GetInt(String.Format(ConstantResources.Records.Score, songTitle.text), 0) });
        maxStreak.SetValue(new object[] { PlayerPrefs.GetInt(String.Format(ConstantResources.Records.MaxStreak, songTitle.text), 0) });
        percentage.SetValue(new object[] { PlayerPrefs.GetInt(String.Format(ConstantResources.Records.Percentage, songTitle.text), 0) });
    }
}
