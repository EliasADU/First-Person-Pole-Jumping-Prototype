using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownCollisionCheck : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float slowDownParameter;

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
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);

            controller.AddImpulse(new Impulse(-cV, slowDownParameter));

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
                slowDownIntervalCounter++;
                Vector3 cV = characterController.velocity;
                Vector3.Normalize(cV);
                controller.AddImpulse(new Impulse(-cV, slowDownParameter));
                slowDownTimer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Slowdown")
        {
            Vector3 cV = characterController.velocity;
            Vector3.Normalize(cV);
            controller.AddImpulse(new Impulse(-cV, slowDownParameter));

            slowDownTimer = 0;
            slowDownIntervalCounter = 0;
        }
    }
}

