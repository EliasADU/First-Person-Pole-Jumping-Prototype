using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    //reference to the player controller
    [SerializeField]
    PlayerController controller;
    
    //reference to the character controller
    [SerializeField]
    CharacterController characterController;

    //reference to the degree of strength
    [SerializeField]
    float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player enters an identified, "Jumpboost" region
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Jumpboost")
        {
            Debug.Log("detected");

            //apply an upward impulse
            Impulse jumpImpulse = new Impulse(Vector3.up, strength);
            controller.AddImpulse(jumpImpulse);
        }
    }
}
