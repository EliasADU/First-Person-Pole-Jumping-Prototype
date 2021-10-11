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
    GameObject allObjects;

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

    [SerializeField]
    float followSharpness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

        allObjects.transform.position = Vector3.Lerp(allObjects.transform.position,
            cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, holdDepth)), blend);
        
        allObjects.transform.rotation = Quaternion.Euler(allObjects.transform.rotation.eulerAngles.x,
            Mathf.LerpAngle(allObjects.transform.rotation.eulerAngles.y, cameraObject.transform.rotation.eulerAngles.y, blend),
            allObjects.transform.rotation.eulerAngles.z);

        allObjects.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(allObjects.transform.rotation.eulerAngles.x, cameraObject.transform.rotation.eulerAngles.x, blend),
            allObjects.transform.rotation.eulerAngles.y,
            allObjects.transform.rotation.eulerAngles.z);

    }
}
