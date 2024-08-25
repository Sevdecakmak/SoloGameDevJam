using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public CharacterController2D mainCharacterController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCharacterController.TeleportGhostParallerlPosition();
        }
    }
}

