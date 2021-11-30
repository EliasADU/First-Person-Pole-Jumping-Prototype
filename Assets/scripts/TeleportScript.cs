using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportTarget;
    public CharacterController player;

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportTarget.transform.position;
    }
}
