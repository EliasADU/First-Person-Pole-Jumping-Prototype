using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;

public class SlowDownMode : MonoBehaviour
{
    [SerializeField]
    PostProcessVolume volume;

    [SerializeField]
    float bloomSpeed;

    Bloom _Bloom;

    [SerializeField]
    float slowDownBloom;

    [SerializeField]
    float normalBloom;

    float target;

    float t;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out _Bloom);

        _Bloom.intensity.value = 0;

        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
            target = slowDownBloom;
            t = 0;
        }
        else
        {
            Time.timeScale = 1f;
            target = normalBloom;
            t = 0;
        }
        moveToTarget();
    }

    void moveToTarget()
    {
        if(_Bloom.intensity.value != target)
        {
            t += Time.deltaTime;
            _Bloom.intensity.value = Mathf.Lerp(_Bloom.intensity.value, target, bloomSpeed * t);
            if(Mathf.Abs(_Bloom.intensity.value - target) <= 0.01)
            {
                _Bloom.intensity.value = target;
            }
        }
    }


}
