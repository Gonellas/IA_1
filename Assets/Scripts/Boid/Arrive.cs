using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : AIBoid
{
    public LayerMask floorMask;
    public string foodTag = "Food";
    public float foodRadius = 1f;
    float maxSpeed = 5f;
    public float arriveDistance = 5f;

    protected override Vector3 CalculateSteering()
    {
        Collider[] foods = Physics.OverlapSphere(transform.position, foodRadius);

        foreach(Collider food in foods)
        {
            if (food.CompareTag(foodTag))
            {
                Vector3 foodDirection = food.transform.position - transform.position;
                float distance = foodDirection.magnitude;
                float speed = maxSpeed;

                if(distance < arriveDistance)
                {
                    speed = maxSpeed * (distance / arriveDistance);
                    Destroy(food.gameObject);
                }

                return foodDirection.normalized * speed;
            }
        }
        return Vector3.zero;
    }

    protected override void Move(Vector3 delta)
    {
        transform.position += delta * Time.deltaTime;
    }

}
