using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMusicController : MonoBehaviourDpm
{
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI songStatusText;
    public TextMeshProUGUI songTimeText;

    public Slider bar;

    public AudioSource audioSource;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }

    private void Start()
    {
        songTitleText.text = SongHolder.Instance.songTitle;
        bar.maxValue = SongHolder.Instance.songTotalTimeMidi;
    }

    private void FixedUpdate()
    {
        songTimeText.text = SecondsToMinutesText(audioSource.time);
        songStatusText.text = SongHolder.Instance.songStatus;
        bar.value = SongHolder.Instance.songCurrentTimeMidi;
    }

    private string SecondsToMinutesText(float seconds)
    {
        int minutesText = (int)seconds / 60;
        int secondsText = (int)seconds % 60;

        return minutesText.ToString("D2") + ":" + secondsText.ToString("D2");
    }
}
