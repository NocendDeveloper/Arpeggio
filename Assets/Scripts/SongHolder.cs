using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * A singleton for load the song and necessary metadata for complete that purpose.
 */
public class SongHolder : MonoBehaviour
{
    [HideInInspector] public string songPath;    
    
    [HideInInspector] public Dictionary<double, List<Note>> song = new ();

    [HideInInspector] public int deltaTicksPerQuarterNote;
    [HideInInspector] public double deltaTime;
    public TempoMap TempoMap;
    
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

    /**
     * Method for clean de data from variables of the class.
     */
    public void ClearData()
    {
        Instance.song = new ();
    }

}
