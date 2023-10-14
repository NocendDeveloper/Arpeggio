using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * A singleton for load the song and necessary metadata for complete that purpose.
 */
public class SongHolder : MonoBehaviourDpm
{
    [HideInInspector] public string midiPath;    
    [HideInInspector] public string mp3Path;
    
    [HideInInspector] public int totalNotes;
    [HideInInspector] public string songTitle;
    [HideInInspector] public long songCurrentTimeMidi;
    [HideInInspector] public long songTotalTimeMidi;
    
    public enum Status { STARTED, PAUSED, STOPPED, FINISHED };
    public Dictionary<Status, string> songStatusDictionary = new ()
    {
        { Status.STARTED, "STARTED" },
        { Status.PAUSED, "PAUSED" },
        { Status.STOPPED, "STOPPED" },
        { Status.FINISHED, "FINISHED" },
    };
    [HideInInspector] public string songStatus = "STATUS";

    public bool isRunning;
    
    private static SongHolder _instance;

    public static SongHolder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SongHolder>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<SongHolder>();
                }
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
