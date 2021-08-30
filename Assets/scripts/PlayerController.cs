using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    //As of August 26th the bulk of this script is retrieved from https://itnext.io/how-to-write-a-simple-3d-character-controller-in-unity-1a07b954a4ca
    //With added comments

    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float speed;

    [SerializeField]
    float verticalSpeed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float minDownwardVelocity;

    List<Impulse> currentImpulses;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentImpulses = new List<Impulse>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ClearFinishedImpulses();
    }

    public void AddImpulse(Impulse i)
    {
        currentImpulses.Add(i);
    }

    //Aggregates all movement on the player, including impulses and applies to gameobject using the character controller
    private void Move()
    {

        ////VERTICAL MOVEMENT////

        if (characterController.isGrounded || verticalSpeed >= 0)
        {
            verticalSpeed = minDownwardVelocity;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

        ////WASD MOVEMENT////
        
        float horizontalMove = Input.GetAxis("Horizontal");
        float forwardsMove = Input.GetAxis("Vertical");

        Vector3 wasdDirection = transform.forward * forwardsMove + transform.right * horizontalMove;
        Vector3 wasdMove = wasdDirection * speed;


        ////TOTAL FROM IMPULSES////
        
        Vector3 impulseMove = GetNetImpulse();


        ////AGGREGATING ALL OTHERS////
        
        characterController.Move((speed * Time.deltaTime * wasdMove) + (gravityMove * Time.deltaTime) + (impulseMove * Time.deltaTime));
    }

    public void ResetGravity()
    {
        verticalSpeed = 0;
    }

    void ClearFinishedImpulses()
    {
        currentImpulses.RemoveAll(s => s.IsZero());
    }

    Vector3 GetNetImpulse()
    {
        Vector3 netImpulse = Vector3.zero;
        foreach (Impulse i in currentImpulses)
        {
            i.Tick();
            netImpulse += i.GetCurrentVelocity();
        }

        return netImpulse;
    }
}
