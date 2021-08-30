using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterController.isGrounded)
            {
                DoJump();
            }
        }
    }

    void DoJump()
    {
        Impulse jumpImpulse = new Impulse(Vector3.up, strength);

        playerController.AddImpulse(jumpImpulse);
    }
}
