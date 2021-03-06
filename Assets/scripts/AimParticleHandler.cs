using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimParticleHandler : MonoBehaviour
{
    [SerializeField]
    GameObject aimingParticlesPrefab;

    //GameObject aimingParticles;

    [SerializeField]
    GameObject blastParticlesPrefab;

    [SerializeField]
    GameObject pullParticlesPrefab;

    GameObject blastParticles;
    GameObject pullParticles;

    //bool particlesStopped = true;

    //ParticleSystem aimingParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void UpdateParticles(Vector3 pos, Vector3 direction)
    //{
    //    if(aimingParticles == null)
    //    {
    //        aimingParticles = Instantiate(aimingParticlesPrefab, Vector3.zero, Quaternion.identity);
    //    }
    //    aimingParticles.transform.forward = -direction;
    //    aimingParticles.transform.position = pos;
    //    aimingParticles.SetActive(true);
    //    particlesStopped = false;
    //}

    public void BlastParticles(Vector3 pos, Vector3 direction)
    {
        blastParticles = Instantiate(blastParticlesPrefab, Vector3.zero, Quaternion.identity);
        blastParticles.transform.forward = direction;
        blastParticles.transform.position = pos;
    }

    public void PullParticles(Vector3 pos, Vector3 direction)
    {
        pullParticles = Instantiate(pullParticlesPrefab, Vector3.zero, Quaternion.identity);
        pullParticles.transform.forward = direction;
        pullParticles.transform.position = pos;
    }

    //public void StopParticles()
    //{
    //    if(particlesStopped == false)
    //    {
    //        DestroyPointer destroyer = aimingParticles.GetComponent<DestroyPointer>();
    //        destroyer.DestroyMe();
    //        aimingParticles = null;
    //        particlesStopped = true;
    //    }
    //}
}
