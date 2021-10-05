using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to apply a speedboost to a player when they enter a speedboost region
public class SpeedboostCollisionCheck : MonoBehaviour
{
    //reference to player controller
    [SerializeField]
    PlayerController controller;

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
        if(other.transform.tag == "Speedboost")
        {
            //add an forward impulse
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(direction, 5));
            timeAtSpeedBoostCollision = Time.realtimeSinceStartup;
            speedBoostIntervalCounter++;
        }
    }

    //while the player remains in the speedboost region
    private void OnTriggerStay(Collider other)
    {
        //in the speedboost region
        if(other.transform.tag == "Speedboost")
        {
            speedBoostTimer += Time.realtimeSinceStartup - timeAtSpeedBoostCollision;
            if(speedBoostTimer >= speedBoostInterval)
            {
                speedBoostIntervalCounter++;
                Vector3 direction = other.transform.forward;
                speedBoostTimer = 0;
                controller.AddImpulse(new Impulse(direction, 5));
            }
        }
    }

    //when player exits the speedboost region
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Speedboost")
        {
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(direction, 5 + speedBoostIntervalCounter));
            speedBoostTimer = 0;
            speedBoostIntervalCounter = 0;
        }
    }
}
