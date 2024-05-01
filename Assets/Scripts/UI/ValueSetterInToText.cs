using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Multimedia;
using TMPro;
using UnityEngine;

public class ValueSetterInToText : MonoBehaviourDpm
{
    /**
     * This variable requires the String.Format() format <br></br>
     * Example: Hola {0}, {1}, {2}, {3}, {4}
     */
    public string constantFormattedString;

    public TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(object[] values, Color color = default)
    {
        textMeshProUGUI.SetText(String.Format(constantFormattedString, values));
        if (color != default) textMeshProUGUI.color = color;
    }
}
