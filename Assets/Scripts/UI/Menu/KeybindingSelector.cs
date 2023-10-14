using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeybindingSelector : MonoBehaviourDpm
{
    /* UI ELEMENTS */
    public string label;
    public TextMeshProUGUI labelText;
    public Button key1Button;
    public TextMeshProUGUI key1Text;
    public Button key2Button;
    public TextMeshProUGUI key2Text;

    /* FUNCTIONAL ELEMENTS */
    [SerializeField] private InputActionReference inputActionReference;
    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        labelText.text = label;
    }

    private void Start()
    {
        SetTextInSelectors();
        SetListeners();
    }

    private void SetTextInSelectors()
    {
        key1Text.text = inputActionReference.action.bindings[0].ToDisplayString();
        key2Text.text = inputActionReference.action.bindings[1].ToDisplayString();
    }

    private void StartRebinding(int key)
    {
        inputActionReference.action.Disable();
        
        _rebindingOperation = inputActionReference.action
            .PerformInteractiveRebinding(key)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete((operation) => CompleteRebinding(key))
            .OnCancel((operation) => CancelRebind())
            .Start();
    }

    private void CompleteRebinding(int key)
    {
        DpmLogger.Log("Rebinding " + inputActionReference.action.name + " key " + key + " to " + inputActionReference.action.bindings[key].overridePath);
        
        _rebindingOperation.Dispose();

        inputActionReference.action.PerformInteractiveRebinding(key);

        // Save Player Prefs
        PlayerPrefs.SetString(inputActionReference.action.id+inputActionReference.action.name+key, inputActionReference.action.bindings[key].overridePath);

        SetTextInSelectors();
        inputActionReference.action.Enable();
    }

    private void CancelRebind()
    {
        _rebindingOperation.Dispose();
        inputActionReference.action.Enable();
    }

    private void SetListeners()
    {
        key1Button.onClick.AddListener(() => StartRebinding(0));
        key2Button.onClick.AddListener(() => StartRebinding(1));
    }
}
