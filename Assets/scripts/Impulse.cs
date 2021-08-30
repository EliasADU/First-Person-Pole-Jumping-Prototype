using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse
{
    Vector3 direction;
    float magnitude;
    float _timeSinceCreation;
    float _drag;

    public float timeSinceCreation
    {
        get
        {
            return _timeSinceCreation;
        }
        set
        {
            _timeSinceCreation = value;
        }
    }
    public float drag
    {
        get
        {
            return _drag;
        }
    }

    public Impulse(Vector3 startDirection, float startMagnitude)
    {
        direction = startDirection;
        magnitude = startMagnitude;
        _drag = 14;
    }

    public void Tick()
    {
        _timeSinceCreation += Time.deltaTime;
    }

    public Vector3 GetCurrentVelocity()
    {
        return (direction * (magnitude - (_timeSinceCreation * drag)));
    }

    public bool IsZero()
    {
        return (magnitude - (_timeSinceCreation * drag) <= 0.01);
    }
}
