using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTIlt : MonoBehaviour
{
    //Editor variables, you can customize these

    [SerializeField]
    float tiltAmount;

    [SerializeField]
    float targetTilt;

    [SerializeField]
    float tiltSpeed;

    public KeyCode leftBtn = KeyCode.A; //A is default
    public KeyCode rightBtn = KeyCode.D; //D is default
    float timeCounter = 0;

    int previousState = 0;
    // Update is called once per frame
    void Update()
    {
        SetTargetTilt();
        LerpToTargetTilt();
    }

    void SetTargetTilt()
    {
        if(Input.GetKey(leftBtn) && Input.GetKey(rightBtn))
        {
            targetTilt = 0;
            ResetTimeIfStateChanged(0);
        }
        else if (Input.GetKey(leftBtn))
        {
            targetTilt = tiltAmount;
            ResetTimeIfStateChanged(1);
        }
        else if (Input.GetKey(rightBtn))
        {
            targetTilt = -tiltAmount;
            ResetTimeIfStateChanged(2);
        }
        else
        {
            targetTilt = 0;
            ResetTimeIfStateChanged(3);
        }
    }

    void ResetTimeIfStateChanged(int state)
    {
        if(previousState != state)
        {
            timeCounter = 0;
        }
        previousState = state;
    }

    void LerpToTargetTilt()
    {
        Quaternion currentRotation = this.transform.rotation;

        if(currentRotation.z != targetTilt)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetTilt);

            timeCounter += Time.deltaTime;

            this.transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, tiltSpeed * timeCounter);

            if (Mathf.Abs(currentRotation.z - targetTilt) <= 0.001)
            {
                this.transform.rotation = targetRotation;
                timeCounter = 0;
            }
        }
    }
}
