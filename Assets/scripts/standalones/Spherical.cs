using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spherical
{
    float _theta, _phi, _r;

    public float theta{ get; set; }
    public float phi { get; set; }
    public float r { get; set; }

    public Spherical(float myTheta, float myPhi, float myR)
    {
        _theta = myTheta;
        _phi = myPhi;
        _r = myR;
    }

    public Spherical(Vector3 cartesian)
    {
        _theta = Mathf.Atan2(cartesian.y, cartesian.x);
        _phi = Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(cartesian.x, 2) + Mathf.Pow(cartesian.y, 2)), cartesian.z);
        _r = (Vector3.Magnitude(cartesian));
    }

    public Vector3 InCartesian()
    {
        Vector3 cartesian = new Vector3();
        cartesian.x = r * Mathf.Sin(_phi) * Mathf.Cos(_theta);
        cartesian.y = r * Mathf.Sin(_phi) * Mathf.Sin(_theta);
        cartesian.z = r * Mathf.Cos(_theta);
        return cartesian;
    }
}
