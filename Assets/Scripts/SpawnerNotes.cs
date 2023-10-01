using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class SpawnerNotes : MonoBehaviour
{
    private DPMLogger _dpmLogger;

    [SerializeField] private GameObject notePrefab;
    private float _speed = 0.7f;

    public AudioSource _speaker;
    private Playback _playback;

    private Vector3 _positionDo = new (-2, 5);
    private Vector3 _positionRe = new (-1, 5);
    private Vector3 _positionMi = new (0, 5);
    private Vector3 _positionFa = new (1, 5);
    private Vector3 _positionSol = new (2, 5);
    
    private double _timer = 0;
    private float _timeInterval = 1f;
    private int _currentNote = 0;
    private bool _playing = false;
    // private int _currentTruckChunk = 0;
    // private int _currentMidiEvent = 0;
    
    // Dictionary notes
    private Dictionary<int, string> _notesDictionary = new ();

    private string[] _notes = { "Do", "Re", "Mi", "Fa", "Sol" };
    private int _noteIndex = 0;
    
    private void Awake()
    {
        UnityThread.initUnityThread();
        InitialiceNotesDictionary();
        _dpmLogger = new DPMLogger(this.name, "#FF5733");
    }

    void Start()
    {
        // _midiFile = new MidiFile(SongHolder.Instance.songPath);
        // _timeInterval = (SongHolder.Instance.deltaTicksPerQuarterNote); // (120 PPQN / 480 PPQN) * (60 segundos / 120 BPM)
        
        StartCoroutine(LoadSongFromMp3());
        _dpmLogger.Log("Notes in the song: " + SongHolder.Instance.song.Count);
    }

    void Update()
    {
        // _dpmLogger.Log("playback time: " + _playback.GetCurrentTime(TimeSpanType.Midi));
        // if (_speaker.clip && _speaker.clip.length > 0f) Playing();
        // if (_playing) Playing();
        // if (_playing) Playing2();
        // if (_playing) Playing3();
        // if (_playing) Playing4();
    }

    private void OnDestroy()
    {
        _playback.Stop();
        _playback.Dispose();
    }

    private void Playing4()
    {
        // var midiFile = MidiFile.Read(ConstantResources.FolderPath + "\\Cry for eternety.mid");
        //
        // var time = _playback.GetCurrentTime(TimeSpanType.Midi);
        // _dpmLogger.Log("snappoints: " + _playback.Snapping);
        
        // var notes = midiFile.GetNotes().AtTime(time, SongHolder.Instance.TempoMap);
        //
        // foreach (var note in notes)
        // {
        //     SpawnNote(note.NoteNumber);
        // }
    }
    
    private void Playing3()
    {
        _playback.NotesPlaybackStarted += (_, e) =>
        {
            try
            {
                UnityThread.executeInUpdate(() => SpawnNote2(e));
            }
            catch (Exception exception)
            {
                _playback.Stop();
                _playback.Dispose();
            }
        };

        long deltaTime = long.Parse(_playback.GetCurrentTime(TimeSpanType.Midi).ToString());
        
        // _dpmLogger.Log("deltatime: " + deltaTime);
        
        SongHolder.Instance.song.TryGetValue(deltaTime, out var noteList);
        if (noteList == null) return;
        
        foreach (var note in noteList) SpawnNote(note.code);
    }

    private void Playing2()
    {
        _timer += Time.deltaTime * SongHolder.Instance.deltaTicksPerQuarterNote;

        _dpmLogger.Log("Time Musical: " + _timer);
        
        SongHolder.Instance.song.TryGetValue((long) _timer, out var noteList);
        if (noteList == null) return;
        
        foreach (var note in noteList) SpawnNote(note.code);
    }

    private void Playing()
    {
        _dpmLogger.Log("time deltaitme: " + Time.deltaTime);
        _timer += Time.deltaTime * _timeInterval;

        if (_timer >= _speed)
        {
            var noteToSpawn = SongHolder.Instance.song[_currentNote];
            
            // SpawnNote(noteToSpawn.code);
            
            // var truckChunk  = _midiFile.Events[_currentTruckChunk] != null ? _midiFile.Events[_currentTruckChunk] : null;
            //
            // if (truckChunk != null)
            // {
            //     if (truckChunk[_currentMidiEvent] != null)
            //     {
            //         var midiEvent = truckChunk[_currentMidiEvent];
            //         if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
            //         {
            //             var noteEvent = (NoteEvent) midiEvent;
            //             SpawnNote(noteEvent.NoteNumber);
            //         }
            //         else
            //         {
            //             SpawnNote(0);
            //         }
            //         _currentMidiEvent++;
            //     }
            //     else
            //     {
            //         _currentTruckChunk++;
            //     }
            // }

            // Reinicia el tiempo transcurrido
            _timer = 0f;
            _currentNote++;
        }
    }

    private void SpawnNote(int noteCode)
    {
        if (noteCode == 999) return;
        
        Vector3 spawnPosition = new();

        switch (_notesDictionary[noteCode])
        {
            case "Do":
                spawnPosition = _positionDo;
                break;
            case "Re":
                spawnPosition = _positionRe;
                break;
            case "Mi":
                spawnPosition = _positionMi;
                break;
            case "Fa":
                spawnPosition = _positionFa;
                break;
            case "Sol":
                spawnPosition = _positionSol;
                break;
        }
        
        Instantiate(notePrefab, spawnPosition, Quaternion.identity);
    }
    
    private NotePlaybackData SpawnNote3(NotePlaybackData notePlayback)
    {
        UnityThread.executeInUpdate(() => SpawnNote(notePlayback.NoteNumber));
        
        return notePlayback;
    }

    private void SpawnNote2(NotesEventArgs notesEventArgs)
    {
        var notes = notesEventArgs.Notes;
        
        // var enumerable = notes as Melanchall.DryWetMidi.Interaction.Note[] ?? notes.ToArray();
        
        foreach (var note in notes)
        {
            // _dpmLogger.Log("note" + note.NoteNumber);
            SpawnNote(note.NoteNumber);
        }
    }

    private void InitialiceNotesDictionary()
    {
        for (int i = 0; i <= 127; i++)
        {
            _notesDictionary.Add(i, _notes[_noteIndex]);
            _noteIndex = (_noteIndex + 1) % _notes.Length;
        }

        foreach (var kvp in _notesDictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }

    private IEnumerator LoadSongFromMp3()
    {
        string mp3Path = SongHolder.Instance.songPath.TrimEnd(ConstantResources.FileExtensionMidi.ToCharArray()) + ConstantResources.FileExtensionMp3;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + mp3Path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Obtener los datos de audio y asignarlos al AudioSource
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                _speaker.clip = audioClip;
                
                // var asioOut = new AsioOut();
                
                var midiFile = MidiFile.Read(ConstantResources.FolderPath + "\\Cry for eternety.mid");
                _playback = midiFile.GetPlayback();
                // _playback = midiFile.GetPlayback(OutputDevice.GetByIndex(0));
                // _playback.NoteCallback = (data, rawTime, length, playbackTime) => SpawnNote3(data);

                Playing3();
                _playback.Start();
                
                StartCoroutine(PlaySongMp3());

                _timeInterval = (SongHolder.Instance.song.Count / _speaker.clip.length);
                _playing = true;
            }
            else
            {
                _dpmLogger.Error("Error loading MP3: " + www.error);
            }
        }

        IEnumerator PlaySongMp3()
        {
            yield return new WaitForSeconds(0.3f);
            _speaker.Play();
        }
    }
}
