using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI streakValue;
    public TextMeshProUGUI multiplierValue;
    
    /* SCORE STATS */
    
    private static ScoreController _instance;

    private long _score;
    private int _multiplier = 1;
    private int _streak;
    private const int ScoreToUp = 10;

    public long Score => _score;

    public int Multiplier => _multiplier;

    public int MultiplierScore => _streak;

    public void ScoreUp()
    {
        // Streak up
        _streak++;
        
        // Multiplier
        CalculateMultiplier();
        
        // Score up
        _score += (ScoreToUp * _multiplier);

        UpdateUI();
    }

    public void CalculateMultiplier()
    {
        if (_streak >= 10) _multiplier = 2;
        if (_streak >= 30) _multiplier = 3;
        if (_streak >= 60) _multiplier = 4;
        if (_streak >= 100) _multiplier = 8;
    }

    public void ResetScore()
    {
        _score = 0;

    }

    public void ResetStreak()
    {
        _streak = 0;
        _multiplier = 1;
    }

    public void ResetAll()
    {
        ResetScore();
        ResetStreak();
    }

    private void UpdateUI() // TODO QUITAR DE AQUÍ SI SE PUEDE HACER COSAS TIPO SUBSCRIBERS O ALGO ASÍ
    {
        scoreValue.SetText(_score.ToString()); 
        streakValue.SetText(_streak.ToString()); 
        multiplierValue.SetText(_multiplier.ToString()); 
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
