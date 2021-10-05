using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for the superjump feature
//players who are already jumping, receive an additional super jump boost
//otherwise, they do not receive a jump boost
public class SuperJump : MonoBehaviour
{
    //reference to the player controller
    [SerializeField]
    PlayerController controller;
    
    //reference to the character controller
    [SerializeField]
    CharacterController characterController;

    //reference to the degree of strength
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

    //when the player enters an identified, "superjump region"
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Superjump")
        {
            verticalSpeed = characterController.velocity.y;

            //minimum velocity to achieve the bounce/superjump feature
            if(verticalSpeed <= -10)
            {
                //apply an upward impulse
                Impulse jumpImpulse = new Impulse(Vector3.up, superStrength);
                controller.AddImpulse(jumpImpulse);
            }
        }
    }

   
}
