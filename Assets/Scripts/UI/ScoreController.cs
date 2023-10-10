using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public enum Actions
    {
        UP,
        UP_NO_STREAK,
        RESET_STREAK,
        RESET_SCORE,
        RESET_ALL
    }
    
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI streakValue;
    public TextMeshProUGUI multiplierValue;
    public TextMeshProUGUI percentageCorrectNotes;
    public StreakMeterController streakMeterController;
    
    /* SCORE STATS */
    
    private static ScoreController _instance;

    private long _score;
    private int _multiplier = 1;
    private int _streak;
    private int _percentageOfCorrectNotes;
    private int _totalNotesCorrect;
    private const int ScoreToUp = 10;

    public long Score => _score;

    public int Multiplier => _multiplier;

    public int MultiplierScore => _streak;

    /**
     * This method controls all the score stats.
     * The objective is execute the UpdateUI() method always.
     */
    public void ScoreControl(Actions action)
    {
        switch (action)
        {
            case Actions.UP:
                ScoreUp();
                break;
            case Actions.UP_NO_STREAK:
                ScoreUpNoStreak();
                break;
            case Actions.RESET_STREAK:
                ResetStreak();
                break;
            case Actions.RESET_SCORE:
                ResetScore();
                break;
            case Actions.RESET_ALL:
                ResetAll();
                break;
        }

        UpdateUI();
    }

    private void CalculateMultiplier()
    {
        if (_streak >= 5) _multiplier = 2;
        if (_streak >= 10) _multiplier = 3;
        if (_streak >= 20) _multiplier = 4;
        if (_streak >= 50) _multiplier = 8;
    }

    private void ScoreUp()
    {
        // Streak up
        _streak++;
        _totalNotesCorrect++;
        
        // Multiplier
        CalculateMultiplier();
        
        // Score up
        _score += (ScoreToUp * _multiplier);
    }
    
    private void ScoreUpNoStreak()
    {
        // Score up
        _score += (ScoreToUp * _multiplier);
    }
    
    private void ResetScore()
    {
        _score = 0;
    }

    private void ResetStreak()
    {
        _streak = 0;
        _multiplier = 1;
    }

    private void ResetAll()
    {
        ResetScore();
        ResetStreak();
    }

    public void CalculatePercentageOfCorrectNotes()
    {
        _percentageOfCorrectNotes = (_totalNotesCorrect / SongHolder.Instance.totalNotes) * 100;
    }

    private void UpdateUI() // TODO QUITAR DE AQUÍ SI SE PUEDE HACER COSAS TIPO SUBSCRIBERS O ALGO ASÍ
    {
        object[] values = { _multiplier.ToString() };
        ValueSetterInToText valueSetterInToText = multiplierValue.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);

        values = new object[] { _streak.ToString() };
        valueSetterInToText = streakValue.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
        
        values = new object[] { _score.ToString() };
        valueSetterInToText = scoreValue.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
        
        values = new object[] { _percentageOfCorrectNotes.ToString() };
        valueSetterInToText = percentageCorrectNotes.GetComponent<ValueSetterInToText>();
        valueSetterInToText.SetValue(values);
        
        streakMeterController.SetStreakMeterValue(_streak);
    }
    
    public static ScoreController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<ScoreController>();
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
