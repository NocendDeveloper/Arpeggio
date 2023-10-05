using System;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviourDpm
{
    public Playback Playback;
    public AudioSource speaker;
    public SpawnerNotes spawnerNotes;

    [HideInInspector] public bool playbackStarted;

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
        Playback.Stop();
        Playback.Dispose();
    }

    private void LoadFiles()
    {
        try
        {
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

        Playback.NotesPlaybackStarted += (_, e) =>
        {
            try
            {
                UnityThread.executeInUpdate(() => spawnerNotes.SpawnNote(e));
            }
            catch (Exception exception)
            {
                Playback.Stop();
                Playback.Dispose();
                DpmLogger.Error("Error trying to create the Playback event: " + exception.Message);
            }
        };

        Playback.Started += (o, e) => playbackStarted = true;
        Playback.Finished += (o, e) =>
        {
            DpmLogger.Log("Song finished");
            ScoreController.Instance.CalculatePercentageOfCorrectNotes();
            playbackStarted = false;
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
