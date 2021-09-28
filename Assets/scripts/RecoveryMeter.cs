using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryMeter : MonoBehaviour
{
    [SerializeField]
    LungeMeterManager lungeMeter;

    [SerializeField]
    float impulseSubtractionAmount;

    [SerializeField]
    float recoveryAmount;

    [SerializeField]
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lungeMeter.UpdateMeter(recoveryAmount * Time.deltaTime);
        if (playerController.AccurateIsGrounded())
        {
            Landed();
        }
    }

    public void ImpulsedSubtraction()
    {
        lungeMeter.UpdateMeter(-impulseSubtractionAmount);
    }

    public void Landed()
    {
        lungeMeter.UpdateMeter(100);
    }

    public bool CanRecover()
    {
        return lungeMeter.MeterIsFull();
    }
}
