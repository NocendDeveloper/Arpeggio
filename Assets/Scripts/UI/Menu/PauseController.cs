using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseController : MonoBehaviourDpm
{
    public Button playPauseButton;
    public Button restartButton;
    public Button backButton;
    
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
        backButton.onClick.AddListener(() => SceneManager.LoadScene(ConstantResources.Scenes.FileBrowserScene));
        
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
        // PAUSE
        if (!_paused) PauseGame();
        // REA-NUDE
        else ResumeGame();
    }

    private void PauseGame()
    {
        DpmLogger.Log("Game paused");
            
        _playPauseButtonSprite = iconPlay;
        Time.timeScale = 0.0f;
        _pausedTime = Time.realtimeSinceStartup;
        
        restartButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        
        // Music controller pause logic
        musicController.PlayPauseSong();
        _paused = true;
    }

    private void ResumeGame()
    {
        startWarning.Play();
        
        DpmLogger.Log("Game resumed");

        _playPauseButtonSprite = iconPause;
        Time.timeScale = 1.0f;
        restartButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        // Music controller pause logic
        musicController.PlayPauseSong();
        _paused = false;
    }
}
