using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationBehaviour : AIBoid
{
    public string boid = "Boid";
    public float separationRadius = 2f;
    public float maxSpeed = 5f;

    protected override Vector3 CalculateSteering()
    {
        Vector3 separationSteering = Vector3.zero;

        Collider[] colliders = Physics.OverlapSphere(transform.position, separationRadius);
        int separationCount = 0;

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Boid") && collider != this)
            {
                separationSteering += transform.position - collider.transform.position;
                separationCount++;
            }
        }

        if(separationCount > 0)
        {
            separationSteering /= separationCount;
        }

        return separationSteering.normalized * maxSpeed;
    }
    protected override void Move(Vector3 delta)
    {
        transform.position += delta * Time.deltaTime;
    }
}
