using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicController : MonoBehaviourDpm
{
    public AudioSource speaker;
    
    public float timeout = 0.3f;
    
    public Button playButton;
    public Button playPauseButton;
    public Sprite iconPlay;
    public Sprite iconPause;
    private Sprite _playPauseButtonSprite;

    private GameplayController _controller;
    private InputAction _pause;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        playButton.onClick.AddListener(PlaySong);
        playPauseButton.onClick.AddListener(PlayPauseSong);

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
        if (_pause.WasPressedThisFrame()) PlayPauseSong();
    }

    private void PlaySong()
    {
        DpmLogger.Log("Playing song... ");
        
        playButton.gameObject.SetActive(false);
        playPauseButton.gameObject.SetActive(true);
        PlaySongMidi();

        // Play with time out.
        StartCoroutine(PlaySongMp3());
    }
    
    public void PlayPauseSong()
    {
        if (SongHolder.Instance.Playback.IsRunning)
        {
            // PAUSE
            DpmLogger.Log("Song paused");
            
            speaker.Pause();
            SongHolder.Instance.Playback.Stop();
            Time.timeScale = 0.0f;
            _playPauseButtonSprite = iconPause;
        }
        else
        {
            DpmLogger.Log("Song resumed");
            
            // REA-NUDE
            speaker.Play();
            SongHolder.Instance.Playback.Start();
            Time.timeScale = 1.0f;
            _playPauseButtonSprite = iconPlay;
        }
    }

    private void PlaySongMidi()
    {
        SongHolder.Instance.Playback.Start();
    }
            
    IEnumerator PlaySongMp3()
    {
        yield return new WaitForSeconds(timeout);
        speaker.Play();
    }
}
