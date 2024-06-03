using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIHunterAgent : MonoBehaviour
{
    protected abstract Vector3 CalculateSteering();
    protected abstract void Movement(Vector3 delta);
    protected virtual void Update()
    {
        Vector3 steering = CalculateSteering();
        Movement(steering);
    }
}
