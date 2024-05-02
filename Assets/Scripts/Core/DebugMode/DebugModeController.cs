using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class DebugModeController : MonoBehaviourDpm
    {
        [SerializeField]
        private InputActionReference inputAction;

        private void Awake()
        {
            SetLogger(name, "#8B8BAE");
        }

        private void Update()
        {
            if (inputAction.action.WasPressedThisFrame()) DebugMode.Instance.SetDebugMode(DebugMode.Instance.IsDebugModeEnabled() ? DebugMode.Mode.DISABLED : DebugMode.Mode.ENABLED);
        }
    }
}