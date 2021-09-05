using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownCollisionCheck : MonoBehaviour
{

    [SerializeField]
    PlayerController controller;

    float slowDownTimer;
    float timeAtSlowDownCollision;
    float slowDownIntervalCounter;

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

        private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Slowdown")
        {
            //use the direction the player is facing to create an impulse that pushes the player backwards
            //this ultimately slows the player down
            Vector3 direction = controller.transform.forward;
            controller.AddImpulse(new Impulse(direction, -8));
            timeAtSlowDownCollision = Time.realtimeSinceStartup;
            slowDownIntervalCounter++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Slowdown")
        {
            slowDownTimer += Time.realtimeSinceStartup - timeAtSlowDownCollision;
            if(slowDownTimer >= slowDownInterval)
            {
                //use the direction the player is facing to create an impulse that pushes the player backwards
                //this ultimately slows the player down
                slowDownIntervalCounter++;
                Vector3 direction = controller.transform.forward;
                slowDownTimer = 0;
                controller.AddImpulse(new Impulse(direction, -8));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Slowbdown")
        {
            //use the direction the player is facing to create an impulse that pushes the player backwards
            //this ultimately slows the player down
            Vector3 direction = controller.transform.forward;
            controller.AddImpulse(new Impulse(direction, -(5 + slowDownIntervalCounter)));
            slowDownTimer = 0;
            slowDownIntervalCounter = 0;
        }
    }
}
