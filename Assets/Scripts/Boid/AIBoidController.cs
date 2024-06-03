using System.Collections.Generic;
using UnityEngine;

public class AIBoidController : MonoBehaviour
{
    private AIBoid _boid;

    void Update()
    {
        if (isNearBoid() && isNearFood())
        {
            SetAgent<SeparationBehaviour>();
            SetAgent<AlignmentAndCohesion>();
            SetAgent<Arrive>();
        }
        else if(isNearHunter() && isNearFood())
        {
            SetAgent<EvadeBehaviour>();
            SetAgent<Arrive>();
        }
    }

    private void SetAgent<T>() where T: AIBoid
    {
        if(_boid != null)
        {
            Destroy(_boid.gameObject);
        }

        GameObject agentObject = new GameObject("AIAgent");
        _boid = agentObject.AddComponent<T>();
    }

    private bool isNearBoid()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Boid") && collider != this)
            {
                return true;
            }
        }
        return false;
    }

    private bool isNearHunter()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag("Hunter"))
            {
                return true;
            }
        }
        return false;
    }
    private bool isNearFood()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Food"))
            {
                return true;
            }
        }
        return false;
    }
}
