using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBindings : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private void Awake()
    {
        Debug.Log("INPUT ACTIONS ASET" + inputActionAsset.FindActionMap("gameplay/Do"));
    }
}
