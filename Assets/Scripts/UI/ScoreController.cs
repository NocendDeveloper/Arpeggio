using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviourDpm
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

    public bool scoreNewRecord = false;
    public bool maxStreakNewRecord = false;
    public bool percentageOfCorrectNotesNewRecord = false;
    
    /* SCORE STATS */
    
    private static ScoreController _instance;

    private long _score;
    private int _multiplier = 1;
    private int _streak;
    private int _maxStreak;
    private int _percentageOfCorrectNotes;
    private int _totalNotesCorrect;
    private const int ScoreToUp = 10;

    public long Score => _score;
    public long MaxStreak => _maxStreak;
    public int Multiplier => _multiplier;
    public int MultiplierScore => _streak;
    public int PercentageOfCorrectNotes => _percentageOfCorrectNotes;

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
        if (_streak > _maxStreak) _maxStreak = _streak;
        
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

    public void OnSongFinish()
    {
        CalculatePercentageOfCorrectNotes();
        UnityThread.executeInUpdate(SaveNewRecords);
    }
    
    private void CalculatePercentageOfCorrectNotes()
    {
        DpmLogger.Log("Calculating percentage of correct notes... ");
        var decimalPercentage = Math.Round(((double)_totalNotesCorrect / (double)SongHolder.Instance.totalNotes), 2);
        DpmLogger.Log("decimalPercentage => " + decimalPercentage);
        _percentageOfCorrectNotes = (int) (decimalPercentage * 100);
        DpmLogger.Log("_percentageOfCorrectNotes => " + _percentageOfCorrectNotes);
    }

    private void SaveNewRecords()
    {
        DpmLogger.Log("Checking new records... ");
        
        int currentScoreRecord = PlayerPrefs.GetInt(String.Format(ConstantResources.Records.Score, SongHolder.Instance.songTitle), 0);
        int currentMaxStreakRecord = PlayerPrefs.GetInt(String.Format(ConstantResources.Records.MaxStreak, SongHolder.Instance.songTitle), 0);
        int currentPercentageRecord = PlayerPrefs.GetInt(String.Format(ConstantResources.Records.Percentage, SongHolder.Instance.songTitle), 0);

        if (_score > currentScoreRecord)
        {
            DpmLogger.Log("Saving new _score record: " + _score);
            PlayerPrefs.SetInt(String.Format(ConstantResources.Records.Score, SongHolder.Instance.songTitle), (int) _score);
            scoreNewRecord = true;
        }

        if (_maxStreak > currentMaxStreakRecord)
        {
            DpmLogger.Log("Saving new _maxStreak record: " + _maxStreak);
            PlayerPrefs.SetInt(String.Format(ConstantResources.Records.MaxStreak, SongHolder.Instance.songTitle), _maxStreak);
            maxStreakNewRecord = true;
        }

        if (_percentageOfCorrectNotes > currentPercentageRecord)
        {
            DpmLogger.Log("Saving new _percentageOfCorrectNotes record: " + _percentageOfCorrectNotes);
            PlayerPrefs.SetInt(String.Format(ConstantResources.Records.Percentage, SongHolder.Instance.songTitle), _percentageOfCorrectNotes);
            percentageOfCorrectNotesNewRecord = true;
        }
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
            SetLogger(name, "#645986");
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
