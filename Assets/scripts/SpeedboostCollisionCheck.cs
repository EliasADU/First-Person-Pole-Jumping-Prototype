using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedboostCollisionCheck : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;

    float speedBoostTimer;
    float timeAtSpeedBoostCollision;
    float speedBoostIntervalCounter;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Speedboost")
        {
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(direction, 5));
            timeAtSpeedBoostCollision = Time.realtimeSinceStartup;
            speedBoostIntervalCounter++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
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

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Speedboost")
        {
            Debug.Log(5 * speedBoostIntervalCounter * 2);
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(direction, 5 + speedBoostIntervalCounter));
            speedBoostTimer = 0;
            speedBoostIntervalCounter = 0;
        }
    }
}
