using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for the slowdown region
public class SlowdownCollisionCheck : MonoBehaviour
{
    //reference to player controller
    [SerializeField]
    PlayerController controller;
    
    //reference to character controller
    [SerializeField]
    CharacterController characterController;

    //reference to the slow down parameter information
    [SerializeField]
    float slowDownParameter;

    //variables to keep track of the time
    float slowDownTimer;
    float timeAtSlowDownCollision;
    float slowDownIntervalCounter;

    //reference to the interval of the slow down 
    [SerializeField]
    float slowDownInterval;

    // Start is called before the first frame update
    void Start()
    {
        slowDownTimer = 0;
        slowDownIntervalCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //entered the slowdown region
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Slowdown")
        {
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);

            //apply a negative impulse, which slows down the player
            controller.AddImpulse(new Impulse(-cV, slowDownParameter));

            //update variables that track the time in the slowdown region
            timeAtSlowDownCollision = Time.realtimeSinceStartup;
            slowDownIntervalCounter++;
        }
    }

    //while the player is in the slowdown region
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Slowdown")
        {
            //update time-related variables
            slowDownTimer += Time.realtimeSinceStartup - timeAtSlowDownCollision;
            if(slowDownTimer >= slowDownInterval)
            {
                slowDownIntervalCounter++;
                Vector3 cV = characterController.velocity;
                Vector3.Normalize(cV);

                //add another negative impulse to slow the player down
                controller.AddImpulse(new Impulse(-cV, slowDownParameter));
                slowDownTimer = 0;
            }
        }
    }

    //when the player exits the slowdown region
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Slowdown")
        {
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);

            //apply one last negative impulse to slow down the player
            controller.AddImpulse(new Impulse(-cV, slowDownParameter));

            slowDownTimer = 0;
            slowDownIntervalCounter = 0;
        }
    }
}

