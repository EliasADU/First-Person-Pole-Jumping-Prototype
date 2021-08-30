using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimParticleHandler : MonoBehaviour
{
    [SerializeField]
    ParticleSystem aimingParticlesPrefab;

    ParticleSystem aimingParticles;

    // Start is called before the first frame update
    void Start()
    {
        aimingParticles = Instantiate(aimingParticlesPrefab, Vector3.zero, Quaternion.identity);
        aimingParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateParticles(Vector3 pos, Vector3 direction)
    {
        if (!aimingParticles.isPlaying)
        {
            aimingParticles.Play();
            //!I believe if this is not inside the if statement the particlesystem will reset after each call
        }
        aimingParticles.transform.forward = -direction;
        aimingParticles.transform.position = pos;
    }

    public void StopParticles()
    {
        aimingParticles.Stop();
    }
}
