using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class MonoBehaviourDpm : MonoBehaviour
{
    protected DPMLogger DpmLogger;

    protected void SetLogger(string className, string color)
    {
        DpmLogger = new DPMLogger(className, color);
    }
}
