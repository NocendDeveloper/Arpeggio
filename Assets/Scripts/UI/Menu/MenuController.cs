using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviourDpm
{
    public InputActionReference actionKey;
    public PauseController pauseController;

    /* CANVAS CONTROL */
    public bool activated;
    private Canvas _canvas;
    public CameraController cameraController;
    
    /* TABS */
    public Button generalTabButton;
    public Button keybindingsTabButton;
    public GameObject generalTab;
    public GameObject keybindingsTab;
    
    /* OPTIONS */
    public GameObject optionsPanel;
    
    private void Awake()
    {
        SetLogger(name, "#53DD6C");
        _canvas = gameObject.GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        actionKey.action.Enable();
        optionsPanel.SetActive(false);
        TabDisableAll();
        if (SceneManager.GetActiveScene().name == "MainGameScene") SetRenderCamera();
    }

    private void OnDisable()
    {
        actionKey.action.Disable(); 
    }
    
    private void Update()
    {
        if (actionKey.action.WasPressedThisFrame()) ActivationControl();
    }

    private void SetRenderCamera()
    {
        string camera = PlayerPrefs.GetString(ConstantResources.Configuration.Cameras.PrefString);

        switch (camera)
        {
            case ConstantResources.Configuration.Cameras.Orthographic:
                _canvas.worldCamera = cameraController.orthographicCamera;
                break;
            case ConstantResources.Configuration.Cameras.Perspective:
                _canvas.worldCamera = cameraController.perspectiveCamera;
                break;
        }
    }

    private void ActivationControl()
    {
        if (activated)
        {
            _canvas.enabled = false;
            activated = false;
        }
        else
        {
            _canvas.enabled = true;
            activated = true;
            if (SceneManager.GetActiveScene().name == "MainGameScene") pauseController.PauseGame();
        }
    }

    #region MAIN BUTTONS

    public void Resume()
    {
        DpmLogger.Log("Resume click");
        ActivationControl();
    }

    public void Songs()
    {
        DpmLogger.Log("Songs click");
        if (SceneManager.GetActiveScene().name != "FileBrowser") SceneManager.LoadScene("FileBrowser");
        ActivationControl();
    }
    
    public void Options()
    {
        DpmLogger.Log("Options click");
        optionsPanel.SetActive(true);
        TabControl(Tabs.GENERAL);
    }
    
    public void Exit()
    {
        DpmLogger.Log("Exit click");
    }
    
    #endregion

    #region TABS

    public enum Tabs
    {
        GENERAL = 0,
        KEYBINDINGS = 1
    }

    public void TabControl(Tabs tab)
    {
        TabControl((int)tab);
    }
    
    public void TabControl(int tab)
    {
        DpmLogger.Log("Tab control: " + tab);
        TabDisableAll();
        
        switch ((Tabs)tab)
        {
            case Tabs.GENERAL:
                generalTab.SetActive(true);
                break;
            case Tabs.KEYBINDINGS:
                keybindingsTab.SetActive(true);
                break;
        }
    }

    private void TabDisableAll()
    {
        generalTab.SetActive(false);
        keybindingsTab.SetActive(false);
    }

    #endregion
}
