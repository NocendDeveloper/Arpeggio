using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class CursorHider: MonoBehaviourDpm
    {
        public float timeForHide = 3f; // Tiempo en segundos antes de ocultar el ratón
        private float timeWithoutMovement = 0f;
        private bool cursorVisible = true;
        
        private void Awake()
        {
            SetLogger(name, ConstantResources.Logs.Colors.CursorHider);
        }

        private void Update()
        {
            HideMouseIfIdle();
        }

        private void Hide()
        {
            DpmLogger.Log("Hiding cursor...");
            Cursor.visible = false;
            cursorVisible = false;
        }

        private void Show()
        {
            DpmLogger.Log("Showing cursor...");
            Cursor.visible = true;
            cursorVisible = true;
        }

        private void HideMouseIfIdle()
        {
            // Verificar si el ratón se ha movido
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                // Si el ratón se ha movido, reiniciar el tiempo de inactividad
                timeWithoutMovement = 0f;

                // Mostrar el ratón si estaba oculto
                if (!cursorVisible) Show();
            }
            else
            {
                // Si el ratón está inactivo, contar el tiempo de inactividad
                timeWithoutMovement += Time.deltaTime;

                // Si el tiempo de inactividad alcanza el límite, ocultar el ratón
                if (timeWithoutMovement >= timeForHide && cursorVisible) Hide();
            }
        }
    }
}