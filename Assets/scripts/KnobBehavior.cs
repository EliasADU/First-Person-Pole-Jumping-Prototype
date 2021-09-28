using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnobBehavior : MonoBehaviour
{
    [SerializeField]
    Image myKnob;

    bool active;

    public bool IsActive()
    {
        return active;
    } 

    public void Lighten()
    {
        myKnob.color = Color.red;
    }

    public void Darken()
    {
        myKnob.color = Color.black;
    }
}
