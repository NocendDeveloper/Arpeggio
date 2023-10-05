using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class StreakMeter : MonoBehaviourDpm
{
    public Slider slider;
    public Color[] colors; // Array of colors corresponding to the checkpoints
    public float[] checkpoints; // Checkpoint values on the Slider

    private Image _sliderFill;

    private void Awake()
    {
        SetLogger(name, "#A5FFD6");
        if (slider == null || colors.Length != checkpoints.Length)
        {
            DpmLogger.Error("Incorrect SliderColorChange configuration. Make sure the Slider is assigned and there is one color for each checkpoint.");
        }
    }

    private void Start()
    {
        _sliderFill = slider.fillRect.GetComponent<Image>();
    }

    private void Update()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (slider.value <= checkpoints[i])
            {
                _sliderFill.color = colors[i];
                break;
            }
        }
    }
}
