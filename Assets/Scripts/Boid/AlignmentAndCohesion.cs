using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentAndCohesion : AIBoid
{
    public LayerMask neighborMask;
    public float cohesionRadius = 5f;
    public float alignmentRadius = 5f;
    public float maxSpeed = 5f;
    protected override Vector3 CalculateSteering()
    {
        Vector3 cohesionPos = Vector3.zero;
        Vector3 alignmentPos = Vector3.zero;
        Collider[] neighbors = Physics.OverlapSphere(transform.position, cohesionRadius, neighborMask);
        
        foreach(Collider neighbor in neighbors)
        {
            cohesionPos += neighbor.transform.position;
            alignmentPos += neighbor.transform.forward;
        }

        if(neighbors.Length > 0)
        {
            cohesionPos /= neighbors.Length;
            alignmentPos /= neighbors.Length;
            return Vector3.zero;
        }

        Vector3 cohesionDirection = (cohesionPos - transform.position).normalized;
        Vector3 totalDirection = (cohesionDirection + alignmentPos).normalized;

        if (neighbors.Length > 0)
        {
            return Vector3.zero;
        }

        totalDirection.y = 0f;
        return totalDirection;
    }

    protected override void Move(Vector3 delta)
    {
        transform.position += delta * Time.deltaTime;
    }
}
