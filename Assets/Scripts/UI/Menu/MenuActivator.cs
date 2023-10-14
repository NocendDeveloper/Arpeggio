using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    public GameObject menu;
    private void Awake()
    {
        menu.SetActive(true);
    }
}
