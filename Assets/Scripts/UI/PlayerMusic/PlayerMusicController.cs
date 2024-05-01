using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMusicController : MonoBehaviourDpm
{
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI songStatusText;
    public TextMeshProUGUI songTimeText;

    public Slider bar;
    public ScoreScreen scoreScreen;

    public AudioSource audioSource;

    private float _timer;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }

    private void OnEnable()
    {
        songTitleText.text = SongHolder.Instance.songTitle;
        bar.maxValue = SongHolder.Instance.songTotalTimeMp3;
    }

    private void Update()
    {
        _timer += Time.deltaTime * 1;
        songStatusText.text = SongHolder.Instance.songStatus;

        if (_timer >= 1)
        {
            songTimeText.text = SecondsToMinutesText(audioSource.time);
            bar.value = audioSource.time;
            _timer = 0;
        }
        
        if (SongHolder.Instance.songStatus.Equals(SongHolder.GetSongStatusString(SongHolder.Status.FINISHED))) scoreScreen.gameObject.SetActive(true);
    }

    private string SecondsToMinutesText(float seconds)
    {
        int minutesText = (int)seconds / 60;
        int secondsText = (int)seconds % 60;

        return minutesText.ToString("D2") + ":" + secondsText.ToString("D2");
    }
}
