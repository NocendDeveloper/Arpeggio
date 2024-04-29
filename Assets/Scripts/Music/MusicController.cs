using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviourDpm
{
    [SerializeField] private MusicLoader musicLoader;
    public AudioSource speaker;
    
    public float timeout = 0.3f;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
    }
    
    // public void PlayPauseSong()
    // {
    //     // START
    //     if (!musicLoader.playbackStarted) StartSong();
    //     // PAUSE    
    //     else if (musicLoader.Playback.IsRunning) PauseSong();
    //     // REA-NUDE
    //     else ResumeSong();
    //     
    //     SetIsRunning();
    // }

    private void StartSong()
    {
        DpmLogger.Log("Starting song... ");
        SongHolder.Instance.SetSongStatus(SongHolder.Status.STARTED);
        
        PlaySongMidi();

        // Play with time out.
        StartCoroutine(PlaySongMp3());
    }
    
    public void PauseSong()
    {
        DpmLogger.Log("Song paused");
        SongHolder.Instance.SetSongStatus(SongHolder.Status.PAUSED);
            
        speaker.Pause();
        musicLoader.Playback.Stop();
    }

    public void ResumeSong()
    {
        if (!musicLoader.playbackStarted)
        {
            StartSong();
            return;
        }
        
        DpmLogger.Log("Song resumed");
        SongHolder.Instance.SetSongStatus(SongHolder.Status.STARTED);
            
        speaker.Play();
        musicLoader.Playback.Start();
    }

    private void PlaySongMidi()
    {
        musicLoader.Playback.Start();
    }
            
    IEnumerator PlaySongMp3()
    {
        yield return new WaitForSeconds(timeout);
        speaker.Play();
    }
}
