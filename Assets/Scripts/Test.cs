using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var midiFile = MidiFile.Read(ConstantResources.FolderPath + "\\Cry for eternety.mid");
        print("Duration: " + midiFile.GetDuration<MetricTimeSpan>());
        print("Notes: " + midiFile.GetNotes());
        print("Notes at time 6555: " + midiFile.GetNotes().AtTime(6555));
        var notes = midiFile.GetNotes();
        var noteattime = midiFile.GetNotes().AtTime(6555);
        var tempomap = midiFile.GetTempoMap();
    }
}
