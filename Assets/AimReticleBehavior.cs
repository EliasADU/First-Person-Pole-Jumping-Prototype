using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimReticleBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject aimingCircle;

    [SerializeField]
    RawImage reticleImage;

    [SerializeField]
    Color activated;

    [SerializeField]
    Color deactivated;

    [SerializeField]
    Vector3 finalSize;

    [SerializeField]
    Vector3 startSize;

    [SerializeField]
    float animationSpeed;

    Color targetColor;
    Vector3 targetSize;

    bool currentState;


    float sizeT = 0;
    float colorT = 0;

    private void Start()
    {
        reticleImage.color = deactivated;
        aimingCircle.transform.localScale = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Activate(false);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Activate(true);
        }
        SizeToTarget();
        ColorToTarget();
    }

    public void Activate(bool s)
    {
        if(s != currentState)
        {
            currentState = s;
            if (s)
            {
                targetColor = activated;
                targetSize = finalSize;
            }
            else
            {
                targetColor = deactivated;
                targetSize = startSize;
            }
            sizeT = 0f;
            colorT = 0f;
        }
    }

    void SizeToTarget()
    {
        sizeT += Time.deltaTime;
        aimingCircle.transform.localScale = Vector3.Lerp(aimingCircle.transform.localScale, targetSize, sizeT * animationSpeed);
    }

    void ColorToTarget()
    {
        colorT += Time.deltaTime;
        reticleImage.color = Vector4.Lerp(reticleImage.color, targetColor, colorT * animationSpeed);
    }

}
