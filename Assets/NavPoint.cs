using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    private void OnEnable()
    {
        References.navPoints.Add(this);
    }

    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }
}
