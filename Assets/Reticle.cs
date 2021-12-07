using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            this.gameObject.SetActive(false);
        }
        if (Time.timeScale == 1)
        {
            this.gameObject.SetActive(true);
        }
    }
}
