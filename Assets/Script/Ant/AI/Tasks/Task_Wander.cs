using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Wander : Task
{
    Vector2 velocitiy;
    public Vector2 desiredDirection;

    public Task_Wander(Ant ant) : base(ant)
    {
    }

    protected override void OnTaskEnd()
    {
    }

    protected override void OnTaskStart()
    {
    }

    public override void Stop()
    {
        ant.task = new Task_ReturnToNest(ant);

    }

    public override void Update()
    {
        if (activityTime > ant.maxActivityTime)
        {
            Stop();
            return;
        }

        activityTime += Time.deltaTime;

        desiredDirection = (desiredDirection + UnityEngine.Random.insideUnitCircle * ant.wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * ant.maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocitiy) * ant.steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, ant.steerStrength) / 1;

        velocitiy = Vector2.ClampMagnitude(velocitiy + acceleration * Time.deltaTime, ant.maxSpeed);
        ant.position += velocitiy * Time.deltaTime;

        float angle = Mathf.Atan2(velocitiy.y, velocitiy.x) * Mathf.Rad2Deg;
        ant.transform.SetPositionAndRotation(ant.position, Quaternion.Euler(0, 0, angle));
    }
}
