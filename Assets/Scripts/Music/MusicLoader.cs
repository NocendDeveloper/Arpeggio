using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class MusicLoader : MonoBehaviourDpm
{
    public AudioSource speaker;
    public SpawnerNotes spawnerNotes;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }

    void Start()
    {
        LoadSongFromMidi();
        StartCoroutine(LoadSongFromMp3());
    }
    
    private void LoadSongFromMidi()
    {
        DpmLogger.Log("Loading song from midi... ");
        
        var midiFile = MidiFile.Read(SongHolder.Instance.midiPath);
        SongHolder.Instance.Playback = midiFile.GetPlayback();

        SongHolder.Instance.Playback.NotesPlaybackStarted += (_, e) =>
        {
            try
            {
                UnityThread.executeInUpdate(() => spawnerNotes.SpawnNote(e));
            }
            catch (Exception exception)
            {
                SongHolder.Instance.Playback.Stop();
                SongHolder.Instance.Playback.Dispose();
            }
        };
    }

    private IEnumerator LoadSongFromMp3()
    {
        DpmLogger.Log("Loading song from mp3... ");

        string mp3Path = SongHolder.Instance.mp3Path;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + mp3Path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                speaker.clip = audioClip;
            }
            else
            {
                DpmLogger.Error("Error loading MP3: " + www.error);
            }
        }
    }

}
