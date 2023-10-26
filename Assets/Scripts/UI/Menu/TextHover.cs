using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextHover : MonoBehaviourDpm, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;
    public Color normalColor;
    private TextMeshProUGUI _textMeshPro;
    
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textMeshPro.color = hoverColor.linear;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textMeshPro.color = normalColor.linear;
    }
}
