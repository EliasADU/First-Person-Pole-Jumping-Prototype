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

    Vector3 oldTargetPosition;
    Vector3 newTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldTargetPosition = cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, holdDepth));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

        newTargetPosition = cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, holdDepth));

        allObjects.transform.position = SuperSmoothLerp(allObjects.transform.position,
            oldTargetPosition, newTargetPosition, Time.deltaTime, 40);

        oldTargetPosition = cameraObject.transform.position + (Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, holdDepth));

        allObjects.transform.rotation = Quaternion.Euler(allObjects.transform.rotation.eulerAngles.x,
            Mathf.LerpAngle(allObjects.transform.rotation.eulerAngles.y, cameraObject.transform.rotation.eulerAngles.y, blend),
            allObjects.transform.rotation.eulerAngles.z);

        allObjects.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(allObjects.transform.rotation.eulerAngles.x, cameraObject.transform.rotation.eulerAngles.x, blend),
            allObjects.transform.rotation.eulerAngles.y,
            allObjects.transform.rotation.eulerAngles.z);

    }

    Vector3 SuperSmoothLerp(Vector3 x0, Vector3 y0, Vector3 yt, float t, float k)
    {
        Vector3 f = x0 - y0 + (yt - y0) / (k * t);
        return yt - (yt - y0) / (k * t) + f * Mathf.Exp(-k * t);
    }
}
