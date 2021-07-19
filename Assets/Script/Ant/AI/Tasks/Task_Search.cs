using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Search : Task
{
    Task_Wander wander_task;
    public Transform searchTarget;
    LayerMask searchLayer;
    Task parent;
    readonly string lookForPheramone;
    readonly string lookForZone;

    public Task_Search(Ant ant, LayerMask searchLayer, string lookForPheramone, Task parent, string lookForZone = null) : base(ant)
    {
        this.searchLayer = searchLayer;
        this.lookForPheramone = lookForPheramone;
        this.lookForZone = lookForZone;
        this.parent = parent;
        OnTaskStart();
    }

    protected override void OnTaskEnd()
    {
        parent.Stop();
    }

    protected override void OnTaskStart()
    {
        wander_task = new Task_Wander(ant);
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        //wander
        wander_task.Update();

        switch (searchTarget)
        {
            case null:
                {
                    // Get all valid objects within the perception radius
                    Collider2D[] allTargets = Physics2D.OverlapCircleAll(ant.position, ant.viewRadius, searchLayer);

                    if (allTargets.Length > 0)
                    {

                        //Select one of the targets objects at random
                        Transform target = allTargets[Random.Range(0, allTargets.Length)].transform;
                        Vector2 dirToTarget = (target.position - ant.transform.position).normalized;

                        // move toward target if can see it
                        if (Vector2.Angle(ant.transform.forward, dirToTarget) < ant.viewAngle / 2)
                        {
                            if (!target.gameObject.CompareTag("Nest"))
                            {
                                target.gameObject.layer = LayerMask.NameToLayer("HeldObject");
                            }
                            searchTarget = target;
                        }
                    }
                    else if (lookForZone != null)
                    {
                        if (ant.IsSensingPheramone(lookForZone, true))
                        {
                            wander_task.desiredDirection = (ant.GetPheramone(lookForZone, true).transform.position - ant.transform.position).normalized;
                        }
                        else if (ant.IsSensingPheramone(lookForPheramone))
                        {
                            wander_task.desiredDirection = (ant.GetPheramone(lookForPheramone).transform.position - ant.transform.position).normalized;
                        }

                    }
                    else
                    {
                        if (ant.IsSensingPheramone(lookForPheramone))
                        {
                            wander_task.desiredDirection = (ant.GetPheramone(lookForPheramone).transform.position - ant.transform.position).normalized;
                        }
                    }

                    break;
                }

            default:
                {

                    wander_task.desiredDirection = (searchTarget.position - ant.transform.position).normalized;

                    const float interactionRadius = 0.05f;
                    if (Vector2.Distance(searchTarget.position, ant.transform.position) < interactionRadius)
                    {
                        OnTaskEnd();
                    }

                    break;
                }
        }
    }
}
