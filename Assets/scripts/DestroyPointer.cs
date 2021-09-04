using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointer : MonoBehaviour
{
    [SerializeField]
    ParticleSystem darkParticles;

    [SerializeField]
    ParticleSystem orangeParticles;


    [SerializeField]
    Light darkLight;

    [SerializeField]
    Light orangeLight;

    [SerializeField]
    float dimSpeed;

    float time;
    bool dimming = false;
    bool dimmed = false;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dimming)
        {
            DimLights();
        }
        if(dimmed && !darkParticles.isPlaying && !orangeParticles.isPlaying)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    public void DestroyMe()
    {
        dimming = true;
        darkParticles.Stop();
        orangeParticles.Stop();
    }

    void DimLights()
    {
        darkLight.intensity = Mathf.Lerp(darkLight.intensity, 0, dimSpeed * time);
        orangeLight.intensity = Mathf.Lerp(orangeLight.intensity, 0, dimSpeed * time);

        if(darkLight.intensity <= 0.01 && orangeLight.intensity <= 0.01)
        {
            dimmed = true;
        }

        time += Time.deltaTime;
    }

}
