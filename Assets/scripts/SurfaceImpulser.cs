using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceImpulser : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    float maxImpulseDistance;

    [SerializeField]
    float impulseStrength;

    [SerializeField]
    ImpulseCharges impulseChargesSetter;

    [SerializeField]
    AimParticleHandler aimParticleHandler;

    // Update is called once per frame
    void Update()
    {
        ResetChargesIfGrounded();
        //returns true during the frame user has pressed left click
        if (Input.GetMouseButtonDown(0) && impulseChargesSetter.HasChargesLeft())
        {
            ImpulseIfPossible();
        }
        if (Input.GetMouseButton(1) && impulseChargesSetter.HasChargesLeft())
        {
            SetAimParticles();
        }
        else
        {
            aimParticleHandler.StopParticles();
        }
    }

    void ResetChargesIfGrounded()
    {
        if (playerController.AccurateIsGrounded())
        {
            impulseChargesSetter.ResetCharges();
        }
    }

    void SetAimParticles()
    {
        Vector3 pointingTo = new Vector3();

        //Executes only if collided with a surface
        if (GetVectorToPointedSurface(out pointingTo))
        {
            if (pointingTo.magnitude <= maxImpulseDistance)
            {
                aimParticleHandler.UpdateParticles(cameraTransform.position - pointingTo, pointingTo);
            }
            else
            {
                aimParticleHandler.StopParticles();
            }
        }
        else
        {
            aimParticleHandler.StopParticles();
        }
    }

    void ImpulseIfPossible()
    {

        Vector3 pointingTo = new Vector3();

        //Executes only if collided with a surface
        if (GetVectorToPointedSurface(out pointingTo))
        {
            if(pointingTo.magnitude <= maxImpulseDistance)
            {
                player.AddImpulse(new Impulse(pointingTo.normalized, impulseStrength));
                impulseChargesSetter.SpendCharge();
                aimParticleHandler.BlastParticles(cameraTransform.position - pointingTo, pointingTo);
                player.ResetGravity();
            }
        }
    }

    //Raycasts to nearest surface and returns Vector3 representing straight line to point from player
    bool GetVectorToPointedSurface(out Vector3 pointingTo)
    {
        RaycastHit collisionPoint;
        var layerMask = ~(1 << 7);
        if(Physics.Raycast(cameraTransform.transform.position, cameraTransform.forward, out collisionPoint, Mathf.Infinity, layerMask))
        {
            if(collisionPoint.collider.gameObject.tag != "Player")
            {
                pointingTo = cameraTransform.transform.position - collisionPoint.point;
                return true;
            }
        }

        pointingTo = Vector3.zero;
        return false;
    }
}
