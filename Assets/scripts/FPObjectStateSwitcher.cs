using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPObjectStateSwitcher : MonoBehaviour
{
    int state = 0;

    [SerializeField]
    GameObject defaultState;

    [SerializeField]
    GameObject aimingState;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
        SwitchStates();
    }

    void SwitchStates()
    {
        switch (state)
        {
            case 0:
                defaultState.SetActive(true);
                aimingState.SetActive(false);
                break;
            case 1:
                defaultState.SetActive(false);
                aimingState.SetActive(true);
                break;
        }
    }
}
