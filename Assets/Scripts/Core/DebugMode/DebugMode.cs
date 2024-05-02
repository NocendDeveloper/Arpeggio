using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class DebugMode : MonoBehaviourDpm
    {
        public enum Mode
        {
            DISABLED,
            ENABLED
        }
        
        private static DebugMode _instance;
        [SerializeField]
        private InputActionReference inputAction;
        [SerializeField]
        private TextMeshProUGUI _enabledIndicator;
        
        private void Awake()
        {
            SetLogger(name, "#8B8BAE");
            DpmLogger.Log("----------------->" + PlayerPrefs.GetInt(ConstantResources.DebugMode.PrefName));
            CheckDebugMode();
        }

        private void Update()
        {
            if (inputAction.action.WasPressedThisFrame()) DebugMode.Instance.SetDebugMode(DebugMode.Instance.IsDebugModeEnabled() ? DebugMode.Mode.DISABLED : DebugMode.Mode.ENABLED);
        }
        
        private void CheckDebugMode()
        {
            if (!PlayerPrefs.HasKey(ConstantResources.DebugMode.PrefName)) SetDebugMode(Mode.DISABLED);
            SetIndicatorProperly();
        }

        public void SetDebugMode(Mode mode)
        {
            DpmLogger.Log("Setting debug mode at: " + mode);
            PlayerPrefs.SetInt(ConstantResources.DebugMode.PrefName, (int) mode);
            SetIndicatorProperly();
        }

        public Mode GetDebugMode()
        {
            return IsDebugModeEnabled() ? Mode.ENABLED : Mode.DISABLED;
        }

        public bool IsDebugModeEnabled()
        {
            return PlayerPrefs.GetInt(ConstantResources.DebugMode.PrefName) == (int) Mode.ENABLED;
        }

        private void SetIndicatorProperly()
        {
            _enabledIndicator.enabled = IsDebugModeEnabled();
        }
        
        public static DebugMode Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DebugMode>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<DebugMode>();
                    }
                }
                return _instance;
            }
        }
    }
}