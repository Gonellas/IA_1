using UnityEngine;

public class PursueBehaviour : AIHunterAgent
{
    [Header("Values")]
    private string _boidsTarget = "Boid";
    public float maxSpeed = 5f;
    public float pursueDistance;

    protected override Vector3 CalculateSteering()
    {
        Vector3 totalPursueForce = Vector3.zero;

        Collider[] colliders = Physics.OverlapSphere(transform.position, pursueDistance);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag(_boidsTarget))
            {
                Vector3 boidPosition = collider.transform.position;
                Vector3 boidDirection = boidPosition - transform.position;

                float distance = boidDirection.magnitude;
                float lookAhead = distance / maxSpeed;

                Vector3 predictedPos = boidPosition + collider.transform.forward * lookAhead;
                Vector3 desiredVelocity = (predictedPos - transform.position).normalized * maxSpeed;

                Vector3 pursueForce = desiredVelocity - (transform.position + totalPursueForce).normalized;

                totalPursueForce += pursueForce;
            }
        }

        return totalPursueForce;
    }

    protected override void Movement(Vector3 delta)
    {
        transform.position += delta * Time.deltaTime;
    }
}
