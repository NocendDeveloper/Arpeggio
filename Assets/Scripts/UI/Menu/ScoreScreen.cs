using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        object[] values = { ScoreController.Instance.Score.ToString() };
        ValueSetterInToText valueSetterInToText = score.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
        
        values = new object[] { ScoreController.Instance.MaxStreak.ToString() };
        valueSetterInToText = maxStreak.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
        
        values = new object[] { ScoreController.Instance.PercentageOfCorrectNotes.ToString() };
        valueSetterInToText = percentage.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
    }
}
