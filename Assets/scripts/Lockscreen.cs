using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockscreen : MonoBehaviour
{
    public GameObject group;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            group.SetActive(true);
            Debug.Log("test");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            group.SetActive(false);
        }
    }
}
