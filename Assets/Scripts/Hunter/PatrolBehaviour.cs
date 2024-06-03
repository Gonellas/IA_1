using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : AIHunterAgent
{
    private string _waypointTag = "Waypoint";
    public float speed = 5f;
    public float waypointRadius = 0.5f;
    int waypointIndex = 0;
    int waypointDir = 1;
    private List<Vector3> waypointsList = new List<Vector3>();
    private AIHunterController hunterController;

    private void Start()
    {
        hunterController = GetComponent<AIHunterController>();
        Patrol();
    }

    private void Patrol()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(_waypointTag))
            {
                waypointsList.Add(collider.transform.position);
            }
        }
    }

    protected override Vector3 CalculateSteering()
    {
        return Vector3.zero;
    }

    protected override void Movement(Vector3 delta)
    {
        if (waypointsList.Count == 0)
            return;

        if (Vector3.Distance(transform.position, waypointsList[waypointIndex]) < waypointRadius)
        {
            waypointIndex += waypointDir;
            if (waypointIndex >= waypointsList.Count || waypointIndex < 0)
            {
                waypointDir *= -1;
                waypointIndex = Mathf.Clamp(waypointIndex, 0, waypointsList.Count - 1);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypointsList[waypointIndex], speed * Time.deltaTime);
    }
}
