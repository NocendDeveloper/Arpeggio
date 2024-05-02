using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviourDpm
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI maxStreak;
    public TextMeshProUGUI percentage;
    
    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }

    private void OnEnable()
    {
        DpmLogger.Log("ScreenScore enabled");
        // object[] values = { ScoreController.Instance.Score.ToString() };
        // ValueSetterInToText valueSetterInToText = score.GetComponent<ValueSetterInToText>();
        // valueSetterInToText.SetValue(values, ScoreController.Instance.scoreNewRecord ? Color.green : default);
        //
        // values = new object[] { ScoreController.Instance.MaxStreak.ToString() };
        // valueSetterInToText = maxStreak.GetComponent<ValueSetterInToText>();
        // valueSetterInToText.SetValue(values);
        //
        // values = new object[] { ScoreController.Instance.PercentageOfCorrectNotes.ToString() };
        // valueSetterInToText = percentage.GetComponent<ValueSetterInToText>();
        // valueSetterInToText.SetValue(values);
        
        SetValueInToText(score, ScoreController.Instance.Score.ToString(), ScoreController.Instance.scoreNewRecord);
        SetValueInToText(maxStreak, ScoreController.Instance.MaxStreak.ToString(), ScoreController.Instance.maxStreakNewRecord);
        SetValueInToText(percentage, ScoreController.Instance.PercentageOfCorrectNotes.ToString(), ScoreController.Instance.percentageOfCorrectNotesNewRecord);
    }

    private void SetValueInToText(TextMeshProUGUI textMeshProUGUI, string text, bool newRecord)
    {
        object[] values = { text };
        ValueSetterInToText valueSetterInToText = textMeshProUGUI.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values, newRecord ? Color.green : default); 
    }
}
