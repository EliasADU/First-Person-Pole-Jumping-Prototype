using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPStaffSpin : MonoBehaviour
{
    [SerializeField]
    GameObject allObjects;

    [SerializeField]
    GameObject staff;

    Vector3 targetSpinRotation;

    [SerializeField]
    float startSpinRotation;

    [SerializeField]
    float finalSpinRotation;

    float targetHeight;

    [SerializeField]
    float startHeightOffset;

    [SerializeField]
    float finalHeightOffset;

    [SerializeField]
    float startStaffOffset;

    bool state;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float heightSpeed;

    float heightT;
    float rotationT;

    Vector3 targetstart;
    Vector3 targetend;

    [SerializeField]
    ParticleSystem darkSlash;
    [SerializeField]
    ParticleSystem fireSlash;
    [SerializeField]
    ParticleSystem darkSlash1;
    [SerializeField]
    ParticleSystem fireSlash1;
    [SerializeField]
    ParticleSystem darkBeam;
    [SerializeField]
    ParticleSystem fireBeam;

    // Start is called before the first frame update
    void Start()
    {
        targetstart = new Vector3(startSpinRotation, staff.transform.localRotation.y, staff.transform.localRotation.z);
        targetend = new Vector3(finalSpinRotation, staff.transform.localRotation.y, staff.transform.localRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            Toggle(true);
        }
        else
        {
            Toggle(false);
        }

        HeightToTarget();
        RotationToTarget();
    }

    public void Toggle(bool s)
    {
        if(state != s)
        {

            state = s;
            heightT = 0;
            rotationT = 0;
            if (state)
            {
                darkSlash.Play();
                fireSlash.Play();
                targetSpinRotation = targetend;
                targetHeight = finalHeightOffset;
            }
            else
            {
                darkSlash1.Play();
                fireSlash1.Play();
                targetSpinRotation = targetstart;
                targetHeight = startHeightOffset;
            }
        }
    }

    void HeightToTarget()
    {
        heightT += Time.deltaTime;
        staff.transform.position = new Vector3(staff.transform.position.x, 
            Mathf.Lerp(staff.transform.position.y, allObjects.transform.position.y + startHeightOffset + targetHeight, heightT * heightSpeed), 
            staff.transform.position.z);
    }

    void RotationToTarget()
    {
        rotationT += Time.deltaTime;
        staff.transform.localRotation = Quaternion.RotateTowards(staff.transform.localRotation, Quaternion.Euler(targetSpinRotation), rotationT * rotationSpeed);
        //staff.transform.localRotation = Quaternion.Euler(Mathf.Lerp(staff.transform.localRotation.eulerAngles.x, targetSpinRotation, rotationT * rotationSpeed),
        //    staff.transform.localRotation.eulerAngles.y,
        //    staff.transform.localRotation.eulerAngles.z);
    }


}
