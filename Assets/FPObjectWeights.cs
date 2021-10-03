using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPObjectWeights : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    MouseLook playerLookScript;

    [SerializeField]
    GameObject cameraObject;

    [SerializeField]
    GameObject staff1;

    float targetXRotation;
    float targetYRotation;
    float targetXRotationV;
    float targetYRotationV;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float holdHeight;
    [SerializeField]
    float holdSide;
    [SerializeField]
    float holdDepth;
    [SerializeField]
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        staff1.transform.position = Vector3.Lerp(staff1.transform.position, 
            cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, holdDepth)), 
            Time.deltaTime * moveSpeed);

        staff1.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(staff1.transform.rotation.eulerAngles.x, cameraObject.transform.rotation.eulerAngles.x, Time.deltaTime * rotationSpeed),
            staff1.transform.rotation.eulerAngles.y,
            staff1.transform.rotation.eulerAngles.z);

        staff1.transform.rotation = Quaternion.Euler(staff1.transform.rotation.eulerAngles.x,
            Mathf.LerpAngle(staff1.transform.rotation.eulerAngles.y, cameraObject.transform.rotation.eulerAngles.y, Time.deltaTime * rotationSpeed),
            staff1.transform.rotation.eulerAngles.z);

    }

    void GetXRotationDueToWeight()
    {

    }
}
