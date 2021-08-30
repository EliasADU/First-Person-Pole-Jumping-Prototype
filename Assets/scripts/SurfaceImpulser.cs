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
    float maxImpulseDistance;

    [SerializeField]
    float impulseStrength;

    [SerializeField]
    AimParticleHandler aimParticleHandler;

    // Update is called once per frame
    void Update()
    {
        //returns true during the frame user has pressed left click
        if (Input.GetMouseButtonDown(0))
        {
            ImpulseIfPossible();
        }
        if (Input.GetMouseButton(1))
        {
            SetAimParticles();
        }
        else
        {
            aimParticleHandler.StopParticles();
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
                //RoundVector(ref pointingTo);

                player.AddImpulse(new Impulse(pointingTo.normalized, impulseStrength));
                player.ResetGravity();
            }
        }
    }

    void RoundVector(ref Vector3 v)
    {
        Spherical s = new Spherical(v);

        s.theta = Mathf.Round(s.theta / (Mathf.PI / 4));
        s.phi = Mathf.Round(s.phi / (Mathf.PI / 4));

        v = s.InCartesian();
    }

    //Raycasts to nearest surface and returns Vector3 representing straight line to point from player
    bool GetVectorToPointedSurface(out Vector3 pointingTo)
    {
        RaycastHit collisionPoint;

        if(Physics.Raycast(cameraTransform.transform.position, cameraTransform.forward, out collisionPoint))
        {
            pointingTo = cameraTransform.transform.position - collisionPoint.point;
            return true;
        }

        pointingTo = Vector3.zero;
        return false;
    }
}
