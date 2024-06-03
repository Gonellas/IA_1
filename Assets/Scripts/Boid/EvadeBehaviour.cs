using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeBehaviour : AIBoid
{
    public string hunter = "Hunter";
    public float evadeRadius = 5f;
    public float maxSpeed = 5f;
    
    protected override Vector3 CalculateSteering()
    {
        Vector3 totalEvadeForce = Vector3.zero;
        Collider[] colliders = Physics.OverlapSphere(transform.position, evadeRadius);

        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag(hunter))
            {
                Vector3 hunterPos = collider.transform.position;
                Vector3 hunterDir = collider.transform.position - transform.position;
                float distance = hunterDir.magnitude;
                float lookAhead = distance / maxSpeed;

                Vector3 predictedPos = hunterPos + collider.transform.forward * lookAhead;
                Vector3 desiredVelocity = (predictedPos - hunterPos).normalized * maxSpeed;
                Vector3 evadeForce = desiredVelocity - (transform.position + totalEvadeForce).normalized;
                totalEvadeForce -= evadeForce;
            }
        }
        totalEvadeForce.y = 0f;
        return totalEvadeForce;
    }

    protected override void Move(Vector3 delta)
    {
        transform.position += delta * Time.deltaTime;
    }
}
