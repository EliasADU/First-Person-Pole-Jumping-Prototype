using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float superStrength;

    float verticalSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Superjump")
        {
            verticalSpeed = characterController.velocity.y;

            //minimum velocity to achieve the bounce/superjump feature
            if(verticalSpeed <= -10)
            {
                Impulse jumpImpulse = new Impulse(Vector3.up, superStrength);
                controller.AddImpulse(jumpImpulse);
            }
        }
    }

   
}
