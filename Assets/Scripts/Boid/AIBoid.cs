using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBoid : MonoBehaviour
{
    protected abstract Vector3 CalculateSteering();
    protected abstract void Move(Vector3 delta);
    protected virtual void Update()
    {
        Vector3 steering = CalculateSteering();
        Move(steering);
    }
}
