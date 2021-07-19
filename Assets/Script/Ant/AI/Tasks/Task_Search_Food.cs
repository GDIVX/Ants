using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Search_Food : Task
{
    Task_Search search_task;

    public Task_Search_Food(Ant ant) : base(ant)
    {
        OnTaskStart();
    }

    protected override void OnTaskEnd()
    {
        search_task.searchTarget.position = ant.transform.position;
        search_task.searchTarget.parent = ant.transform;
        search_task.searchTarget.gameObject.layer = LayerMask.NameToLayer("HeldObject");
        search_task. searchTarget = null;

        ant.task = new Task_ReturnToNest(ant);

    }

    protected override void OnTaskStart()
    {
        search_task = new Task_Search(ant, LayerMask.GetMask("Food"), "Way To Food" , this , "Food Zone");
    }

    public override void Stop()
    {
        OnTaskEnd();
    }

    public override void Update()
    {
        //search for food
        search_task.Update();
        if (activityTime <= ant.maxActivityTime)
        {
            ant.PlacePheramone("Way To Nest", Color.blue);
        }
        else
        {
            ant.task = new Task_ReturnToNest(ant);
            return;
        }
        activityTime += Time.deltaTime;

    }
}
