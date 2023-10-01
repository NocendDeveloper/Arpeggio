using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Melanchall.DryWetMidi.Interaction;
using MPTK.NAudio.Midi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidiReader : MonoBehaviour
{
    private DPMLogger _dpmLogger;
    
    private void Awake()
    {
        _dpmLogger = new DPMLogger(this.name, "#FF5733");
    }
    
    void Start()
    {
        // ProcessMidiFile();
        ProcessMidiFile2();
    }

    private void ProcessMidiFile()
    {
        _dpmLogger.Log("Processing midi file...");

        // Abre el archivo MIDI
        var midiFile = new MidiFile(SongHolder.Instance.songPath);
        _dpmLogger.Log("Song loaded: " + SongHolder.Instance.songPath);
        
        _dpmLogger.Log("Delta ticks per quarter note: " + midiFile.DeltaTicksPerQuarterNote);
        SongHolder.Instance.deltaTicksPerQuarterNote = midiFile.DeltaTicksPerQuarterNote;

        _dpmLogger.Log("Tracks: " + midiFile.Tracks);
        // Itera a través de las pistas del archivo MIDI
        foreach (var trackChunk in midiFile.Events)
        {
            // Itera a través de los eventos en la pista actual
            foreach (MidiEvent midiEvent in trackChunk) LoadSongOnSongHolder(midiEvent);
        }
        
        _dpmLogger.Log("Midi file processed.");

        // Load Main Game Scene
        SceneManager.LoadScene(ConstantResources.Scenes.MainGameScene);
    }

    private void ProcessMidiFile2()
    {
        _dpmLogger.Log("Processing midi file...");

        // Abre el archivo MIDI
        var midiFile = Melanchall.DryWetMidi.Core.MidiFile.Read(SongHolder.Instance.songPath);
        _dpmLogger.Log("Song loaded: " + SongHolder.Instance.songPath);
        
        _dpmLogger.Log("Delta ticks per quarter note: " + midiFile.TimeDivision);

        SongHolder.Instance.TempoMap = midiFile.GetTempoMap();

        // Itera a través de las pistas del archivo MIDI
        foreach (var note in midiFile.GetNotes())
        {
            LoadSongOnSongHolder2(note);
        }
        
        _dpmLogger.Log("Midi file processed.");

        // Load Main Game Scene
        SceneManager.LoadScene(ConstantResources.Scenes.MainGameScene);
    }

    private void LoadSongOnSongHolder2(Melanchall.DryWetMidi.Interaction.Note note)
    {
        int noteCode = note.NoteNumber;
        var deltaTime = note.Time;

        var noteToAdd = gameObject.AddComponent<Note>();
        
        noteToAdd.code = noteCode;
        noteToAdd.on = true;

        if (SongHolder.Instance.song.TryGetValue(deltaTime, out var noteList)) noteList.Add(noteToAdd);
        else SongHolder.Instance.song.Add(deltaTime, new List<Note> { noteToAdd });
    }
    
    private void LoadSongOnSongHolder(MidiEvent midiEvent)
    {
        if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
        {
            var noteEvent = (NoteEvent) midiEvent;
            int noteCode = noteEvent.NoteNumber;
            
            var deltaTime = noteEvent.AbsoluteTime; // / SongHolder.Instance.deltaTicksPerQuarterNote;

            var noteToAdd = gameObject.AddComponent<Note>();
            noteToAdd.code = noteCode;
            noteToAdd.on = true;

            if (SongHolder.Instance.song.TryGetValue(deltaTime, out var noteList)) noteList.Add(noteToAdd);
            else SongHolder.Instance.song.Add(deltaTime, new List<Note> { noteToAdd });
        }
        
        
        // else if (midiEvent.CommandCode == MidiCommandCode.NoteOff)
        // {
        //     var noteEvent = (NoteEvent) midiEvent;
        //     int noteCode = noteEvent.NoteNumber;
        //     
        //     var cosa = 1.000000000 * (noteEvent.AbsoluteTime / (double) SongHolder.Instance.deltaTicksPerQuarterNote);
        //     if (cosa == 0) return;
        //
        //     var noteToAdd = gameObject.AddComponent<Note>();
        //     noteToAdd.code = noteCode;
        //     noteToAdd.on = false;
        //
        //     SongHolder.Instance.song.Add(noteToAdd);
        // }
    }
}
