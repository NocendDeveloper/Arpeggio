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

    // Dictionary notes
    private readonly Vector3 _positionDo = new (-3, 20);
    private readonly Vector3 _positionRe = new (-1.5f, 20);
    private readonly Vector3 _positionMi = new (0, 20);
    private readonly Vector3 _positionFa = new (1.5f, 20);
    private readonly Vector3 _positionSol = new (3, 20);
    
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
        
        // DpmLogger.Log("Nota sonando => " + (noteMidi.EndTime - noteMidi.Time));
        
        Note note = notePool.GetItem(_notesDictionary[noteMidi.NoteNumber], Quaternion.identity).GetComponent<Note>();

        note.SetColor();
    }

    private void InitialiceNotesDictionary()
    {
        DpmLogger.Log("Initializating notes dictionary... ");
        
        var noteIndex = 0;
        _notesPositions = new []{ _positionDo, _positionRe, _positionMi, _positionFa, _positionSol };
        
        for (var i = 0; i <= 127; i++)
        {
            _notesDictionary.Add(i, _notesPositions[noteIndex]);
            noteIndex = (noteIndex + 1) % _notesPositions.Length;
        }
    }
}
