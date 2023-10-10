using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviourDpm
{
    public InputAction actionKey;

    /* CANVAS CONTROL */
    private bool _activated;
    private Canvas _canvas;
    
    /* BUTTONS CONTROL */
    public Button buttonResume;
    public Button buttonOptions;
    public Button buttonExit;
    
    /* OPTIONS */
    public new TMP_Dropdown.DropdownEvent camera;
    
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        _canvas = gameObject.GetComponent<Canvas>();
        _canvas.enabled = false;
        SetListeners();
    }

    private void OnEnable()
    {
        actionKey.Enable();
    }

    private void OnDisable()
    {
        actionKey.Disable(); 
    }
    
    private void Update()
    {
        if (actionKey.WasPressedThisFrame()) ActivationControl();
    }

    private void ActivationControl()
    {
        if (_activated)
        {
            _canvas.enabled = false;
            _activated = false;
        }
        else
        {
            _canvas.enabled = true;
            _activated = true;
        }
    }

    private void SetListeners()
    {
        buttonResume.onClick.AddListener(Resume);
        buttonOptions.onClick.AddListener(Options);
        buttonExit.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        DpmLogger.Log("Resume click");
        ActivationControl();
    }
    
    private void Options()
    {
        DpmLogger.Log("Options click");
    }
    
    private void Exit()
    {
        DpmLogger.Log("Exit click");
    }
}
