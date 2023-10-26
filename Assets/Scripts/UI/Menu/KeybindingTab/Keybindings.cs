using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Reflection;


public class Keybindings : MonoBehaviourDpm
{
    public InputActionAsset inputActionAsset;

    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        LoadBindings();
        // DpmLogger.Log(inputActionAsset.FindActionMap("gameplay").FindAction("Re").GetBindingDisplayString());
        // DpmLogger.Log(inputActionAsset.FindActionMap("gameplay").FindAction("Mi").GetBindingDisplayString());
        // DpmLogger.Log(inputActionAsset.FindActionMap("gameplay").FindAction("Fa").GetBindingDisplayString());
        // DpmLogger.Log(inputActionAsset.FindActionMap("gameplay").FindAction("Sol").GetBindingDisplayString());
    }

    private void LoadBindings()
    {
        DpmLogger.Log("Loading bindings from PlayerPrefs...");

        foreach (var inputAction in inputActionAsset)
        {
            string key1 = PlayerPrefs.GetString(inputAction.id+inputAction.name+0);
            string key2 = PlayerPrefs.GetString(inputAction.id+inputAction.name+1);
            if (!key1.Equals("")) inputAction.ApplyBindingOverride(0, key1);
            if (!key2.Equals("")) inputAction.ApplyBindingOverride(1, key2);
        }
    }
}
