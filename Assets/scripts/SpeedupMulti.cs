using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to apply a speedboost to a player when they enter a speedboost region
public class SpeedupMulti : MonoBehaviour
{
    //reference to player controller
    [SerializeField]
    PlayerController controller;

    //reference to character controller
    [SerializeField]
    CharacterController characterController;

    //reference to speedboost amount
    [SerializeField]
    float speedboostParameter;

    //variables used to keep track of the speedboost
    float speedBoostTimer;
    float timeAtSpeedBoostCollision;
    float speedBoostIntervalCounter;

    //reference for the speedBoost interval
    [SerializeField]
    float speedBoostInterval;

    // Start is called before the first frame update
    void Start()
    {
        speedBoostTimer = 0;
        speedBoostIntervalCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player enters a speedboost region
    private void OnTriggerEnter(Collider other)
    {
        //entered speedboost region
        if(other.transform.tag == "Speedboost Multi")
        {
            //add an impulse
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);

            //apply a negative impulse, which slows down the player
            controller.AddImpulse(new Impulse(cV, speedboostParameter));
            timeAtSpeedBoostCollision = Time.realtimeSinceStartup;
            speedBoostIntervalCounter++;
        }
    }

    //while the player remains in the speedboost region
    private void OnTriggerStay(Collider other)
    {
        //in the speedboost region
        if(other.transform.tag == "Speedboost Multi")
        {
            speedBoostTimer += Time.realtimeSinceStartup - timeAtSpeedBoostCollision;
            if(speedBoostTimer >= speedBoostInterval)
            {
                Vector3 cV = characterController.velocity;
                Vector3.Normalize(cV);

                speedBoostIntervalCounter++;
                speedBoostTimer = 0;
                controller.AddImpulse(new Impulse(cV, speedboostParameter));
            }
        }
    }

    //when player exits the speedboost region
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Speedboost Multi")
        {
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);
            controller.AddImpulse(new Impulse(cV, speedboostParameter));
            speedBoostTimer = 0;
            speedBoostIntervalCounter = 0;
        }
    }
}
