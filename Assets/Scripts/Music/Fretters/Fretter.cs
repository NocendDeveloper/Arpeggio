using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Fretter : FretterCore
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note")) UnFret();
    }
    
    /**
     * This method controls that the player dont hold de button for take all notes.
     */
    protected override void FretControl(InputAction inputAction)
    {
        if (collider.enabled) timer += Time.deltaTime * 1f;
        
        if (timer > unFretAt) UnFret();
        if (inputAction.WasPressedThisFrame()) Fret();
        
        if (inputAction.WasReleasedThisFrame()) renderer.material = colors[1];
    }

    private void Fret()
    {
        // if (!CheckCollisionWithNote()) DpmLogger.Log("Falláste jajáaa");

        collider.enabled = true;
        renderer.material = colors[0];
    }

    private void UnFret()
    {
        collider.enabled = false;
        timer = 0;
    }
    
    // private bool CheckCollisionWithNote()
    // {
    //     // La posición desde donde se lanza el Raycast puede ser el centro del jugador o cualquier punto que desees.
    //     var transform1 = transform;
    //     Vector3 raycastOrigin = transform1.position;
    //
    //     // La dirección del Raycast, puede ser hacia adelante o en la dirección que necesites.
    //     Vector3 raycastDirection = transform1.forward;
    //
    //     // La longitud máxima del Raycast, ajusta esto según tus necesidades.
    //     float raycastDistance = 0.3f;
    //
    //     Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red);
    //     
    //     // Lanzar el Raycast y verificar si colisiona con objetos con la etiqueta "Note".
    //     RaycastHit hit;
    //     if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
    //     {
    //         if (hit.collider.isTrigger)
    //         {
    //             // El jugador ha acertado a una nota.
    //             return true;
    //         }
    //     }
    //
    //     // El jugador no ha acertado a ninguna nota.
    //     return false;
    // }
}
