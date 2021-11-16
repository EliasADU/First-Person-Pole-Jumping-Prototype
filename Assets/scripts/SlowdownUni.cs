using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownUni : MonoBehaviour
{
    //reference to player controller
    [SerializeField]
    PlayerController controller;

    //reference to slowdown amount
    [SerializeField]
    float slowdownParameter;

    //variables used to keep track of the slowdown
    float slowdownTimer;
    float timeAtslowdownCollision;
    float slowdownIntervalCounter;

    //reference for the slowdown interval
    [SerializeField]
    float slowdownInterval;

    // Start is called before the first frame update
    void Start()
    {
        slowdownTimer = 0;
        slowdownIntervalCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player enters a slowdown region
    private void OnTriggerEnter(Collider other)
    {
        //entered slowdown region
        if(other.transform.tag == "Slowdown Uni")
        {
            Debug.Log("Entered");
            //add a backward impulse
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(-direction, slowdownParameter));
            timeAtslowdownCollision = Time.realtimeSinceStartup;
            slowdownIntervalCounter++;
        }
    }

    //while the player remains in the slowdown region
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        //in the slowdown region
        if(other.transform.tag == "Slowdown Uni")
        {
            slowdownTimer += Time.realtimeSinceStartup - timeAtslowdownCollision;
            if(slowdownTimer >= slowdownInterval)
            {
                slowdownIntervalCounter++;
                Vector3 direction = other.transform.forward;
                slowdownTimer = 0;
                controller.AddImpulse(new Impulse(-direction, slowdownParameter));
            }
        }
    }

    //when player exits the slowdown region
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.transform.tag == "Slowdown Uni")
        {
            Vector3 direction = other.transform.forward;
            controller.AddImpulse(new Impulse(-direction, slowdownParameter));
            slowdownTimer = 0;
            slowdownIntervalCounter = 0;
        }
    }
}
