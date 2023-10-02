using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuController : MonoBehaviourDpm
{
    public Button playPauseButton;
    public Button restartButton;
    public Sprite iconPlay;
    public Sprite iconPause;
    private Sprite _playPauseButtonSprite;

    [SerializeField] private MusicController musicController;
    
    private GameplayController _controller;

    private InputAction _pause;
    private bool _paused = true;
    private float _pausedTime = 0.0f;
    
    public int secondsToWaitForStart;
    public AudioSource startWarning;


    private void Awake()
    {
        SetLogger(name, "#FF686B");

        playPauseButton.onClick.AddListener(PlayPause);
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(ConstantResources.Scenes.MainGameScene));
        _playPauseButtonSprite = playPauseButton.GetComponent<Image>().sprite;
        _controller = new GameplayController();
    }
    
    private void OnEnable()
    {
        _pause = _controller.gameplay.PauseResume;
        _pause.Enable();
    }

    private void OnDisable()
    {
        _pause.Disable();
    }

    private void Update()
    {
        if (_pause.WasPressedThisFrame()) PlayPause();
    }

    private void PlayPause()
    {
        DpmLogger.Log("_paused: " + _paused);
        
        // PAUSE
        if (!_paused) PauseGame();
        // REA-NUDE
        else ResumeGame();
    }

    private void PauseGame()
    {
        DpmLogger.Log("Game paused");
            
        Time.timeScale = 0.0f;
        _pausedTime = Time.realtimeSinceStartup;
        
        _playPauseButtonSprite = iconPlay;
        restartButton.gameObject.SetActive(true);
        
        // Music controller pause logic
        musicController.PlayPauseSong();
        _paused = true;
    }

    private void ResumeGame()
    {
        startWarning.Play();
        
        DpmLogger.Log("Game resumed");

        Time.timeScale = 1.0f;
        _playPauseButtonSprite = iconPause;
        restartButton.gameObject.SetActive(false);

        // Music controller pause logic
        musicController.PlayPauseSong();
        _paused = false;
    }
}
