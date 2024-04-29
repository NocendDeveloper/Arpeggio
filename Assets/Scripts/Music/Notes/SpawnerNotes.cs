using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpawnerNotes : MonoBehaviourDpm
{
    public ItemPool notePool;
    public ItemPool noteStartPool;
    public ItemPool noteEndPool;
    public ItemPool noteHoldersPool;

    public int octaveActivated = 7;

    // Dictionary notes
    private readonly Vector3 _positionDo = new (-3, 20, -0.1f);
    private readonly Vector3 _positionRe = new (-1.5f, 20, -0.1f);
    private readonly Vector3 _positionMi = new (0, 20, -0.1f);
    private readonly Vector3 _positionFa = new (1.5f, 20, -0.1f);
    private readonly Vector3 _positionSol = new (3, 20, -0.1f);
    
    private readonly Dictionary<int, Vector3> _notesDictionary = new ();
    private Vector3[] _notesPositions;
    
    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        UnityThread.initUnityThread();
        InitialiceNotesDictionary();
    }

    public void SpawnNote(NotesEventArgs notesEventArgs)
    {
        var noteMidi = notesEventArgs.Notes.FirstOrDefault();

        if (noteMidi == null) return;
        
        Note note = notePool.GetItem(_notesDictionary[noteMidi.NoteNumber], Quaternion.identity).GetComponent<Note>();
        note.note = noteMidi.NoteNumber;
        note.octave = noteMidi.Octave;
        note.length = noteMidi.Length;
        
        // if (noteMidi.NoteNumber <= 86)
        if (noteMidi.Octave != octaveActivated) //TODO: DIFICULTY SELECTOR
        {
            SongHolder.Instance.totalNotes--;
            note.gameObject.SetActive(false);
        }
        else if (noteMidi.Length > 100)
        {
            NoteStart noteStart = noteStartPool.GetItem(_notesDictionary[noteMidi.NoteNumber], Quaternion.identity).GetComponent<NoteStart>();
            noteStart.trailRenderer.time = (0.3f * noteMidi.Length) / 240f;
            noteStart.SetTrailColor();
        }
        
        note.SetColor();
    }

    public void SpawnNoteEnd(NotesEventArgs notesEventArgs)
    {
        var noteMidi = notesEventArgs.Notes.FirstOrDefault();

        if (noteMidi == null) return;
        
        if (noteMidi.Octave == octaveActivated) noteEndPool.GetItem(_notesDictionary[noteMidi.NoteNumber], Quaternion.identity);
    }

    private void InitialiceNotesDictionary()
    {
        DpmLogger.Log("Initializating notes dictionary... ");
        
        var noteIndex = 0;

        switch (PlayerPrefs.GetString(ConstantResources.Configuration.MusicSheet.PrefString))
        {
            case ConstantResources.Configuration.MusicSheet.Original:
                _notesPositions = new []{ _positionSol, _positionDo, _positionRe, _positionMi, _positionFa };
                break;
            case ConstantResources.Configuration.MusicSheet.Matias:
                _notesPositions = new []{ _positionDo, _positionRe, _positionMi, _positionFa, _positionSol };
                break;
        }
            

        for (var i = 0; i <= 127; i++)
        {
            _notesDictionary.Add(i, _notesPositions[noteIndex]);
            noteIndex = (noteIndex + 1) % _notesPositions.Length;
        }
    }
}
