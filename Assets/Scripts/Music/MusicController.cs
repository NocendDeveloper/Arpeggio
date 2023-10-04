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
    
    public void PlayPauseSong()
    {
        // START
        if (!musicLoader.playbackStarted) StartSong();
        // PAUSE    
        else if (musicLoader.Playback.IsRunning) PauseSong();
        // REA-NUDE
        else ResumeSong();
        
        SetIsRunning();
    }

    private void StartSong()
    {
        DpmLogger.Log("Starting song... ");
        
        PlaySongMidi();

        // Play with time out.
        StartCoroutine(PlaySongMp3());
    }
    
    private void PauseSong()
    {
        DpmLogger.Log("Song paused");
            
        speaker.Pause();
        musicLoader.Playback.Stop();
    }

    private void ResumeSong()
    {
        DpmLogger.Log("Song resumed");
            
        speaker.Play();
        musicLoader.Playback.Start();
    }

    private void SetIsRunning()
    {
        SongHolder.Instance.isRunning = musicLoader.Playback.IsRunning;
    }

    private void PlaySongMidi()
    {
        musicLoader.Playback.Start();
        SetIsRunning();
    }
            
    IEnumerator PlaySongMp3()
    {
        yield return new WaitForSeconds(timeout);
        speaker.Play();
    }
}
