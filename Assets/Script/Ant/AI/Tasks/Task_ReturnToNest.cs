using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_ReturnToNest : Task
{
    Task_Search search_task;

    public Task_ReturnToNest(Ant ant) : base(ant)
    {
        OnTaskStart();
    }

    protected override void OnTaskEnd()
    {
        if (ant.transform.childCount > 0)
        {
            Transform heldObject = ant.transform.GetChild(0);
            heldObject.position = search_task.searchTarget.position;
            heldObject.parent = search_task.searchTarget.transform;

        }
        search_task.searchTarget = null;
        ant.task = new Task_Search_Food(ant);
    }

    protected override void OnTaskStart()
    {
        search_task = new Task_Search(ant, LayerMask.GetMask("Nest"), "Way To Nest", this , "Colony Zone");
    }

    public override void Stop()
    {
        OnTaskEnd();
    }

    public override void Update()
    {
        search_task.Update();

        if (ant.transform.childCount > 0 && activityTime <= ant.maxActivityTime)
        {
            ant.PlacePheramone("Way To Food", Color.red);
        }
        else
        {
            if (ant.transform.childCount > 0)
            {
                Transform heldObject = ant.transform.GetChild(0);
                heldObject.position = ant.position;
                heldObject.gameObject.layer = LayerMask.GetMask("Food");
                heldObject.parent = null;
            }



            ant.task = new Task_Search_Food(ant);
            return;
        }
        activityTime += Time.deltaTime;
    }
}
