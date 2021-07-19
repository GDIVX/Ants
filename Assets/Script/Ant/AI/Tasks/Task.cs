using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    protected Ant ant;

    protected float activityTime;

    protected Task(Ant ant)
    {
        this.ant = ant;
        activityTime = 0;
    }

    protected abstract void OnTaskStart();
    protected abstract void OnTaskEnd();
    public abstract void Update();
    public abstract void Stop();
}


