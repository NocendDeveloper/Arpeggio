using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Tools;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviourDpm
{
    public Playback Playback;
    public AudioSource speaker;
    public SpawnerNotes spawnerNotes;
    
    [HideInInspector] public bool playbackStarted;
    [HideInInspector] public bool mp3Loaded;
    [HideInInspector] public bool midiLoaded;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }

    void Start()
    {
        LoadFiles();
    }
    
    private void OnDestroy()
    {
        DestroyPlayback();
    }

    private void LoadFiles()
    {
        try
        {
            mp3Loaded = false;
            midiLoaded = false;
            LoadSongFromMidi();
            StartCoroutine(LoadSongFromMp3());
        }
        catch (Exception e)
        {
            SceneManager.LoadScene(ConstantResources.Scenes.FileBrowserScene);
        }
    }
    
    private void LoadSongFromMidi()
    {
        DpmLogger.Log("Loading song from midi... ");
        
        var midiFile = MidiFile.Read(SongHolder.Instance.midiPath);

        SongHolder.Instance.totalNotes = midiFile.GetNotes().Count;
        
        Playback = midiFile.GetPlayback();
        
        SongHolder.Instance.songTotalTimeMidi = long.Parse(Playback.GetDuration(TimeSpanType.Midi).ToString());

        Playback.NotesPlaybackStarted += (_, e) =>
        {
            try
            {
                // SongHolder.Instance.songCurrentTimeMidi = long.Parse(Playback.GetCurrentTime(TimeSpanType.Midi).ToString());
                UnityThread.executeInUpdate(() => spawnerNotes.SpawnNote(e));
            }
            catch (Exception exception)
            {
                DestroyPlayback();
                DpmLogger.Error("Error trying to create the Playback event: " + exception.Message);
            }
        };

        Playback.NotesPlaybackFinished += (_, e) =>
        {
            UnityThread.executeInUpdate(() => spawnerNotes.SpawnNoteEnd(e));
        };

        Playback.Started += (o, e) => playbackStarted = true;
        Playback.Finished += (o, e) =>
        {
            DpmLogger.Log("Song finished");
            ScoreController.Instance.OnSongFinish();
            SongHolder.Instance.SetSongStatus(SongHolder.Status.FINISHED);
            playbackStarted = false;
        };
        
        midiLoaded = true;
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
                SongHolder.Instance.songTotalTimeMp3 = speaker.clip.length;
            }
            else
            {
                DpmLogger.Error("Error loading MP3: " + www.error);
            }
        }

        mp3Loaded = true;
    }

    /**
     * Force the song to end.
     */
    public void FinishSong()
    {
        Playback.MoveToTime(Playback.GetDuration(TimeSpanType.Metric));
    }

    public void DestroyPlayback()
    {
        if (Playback != null)
        {
            Playback.Stop();
            Playback.Dispose();
        }
    }
}
