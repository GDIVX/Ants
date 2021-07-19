using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Needs : MonoBehaviour
{
    public Need hunger;
    public Need energy;

    private void Awake()
    {
        hunger = new Need(20);
        energy = new Need(20);
    }

    private void Update()
    {
        hunger.DepleateOverTime(.8f);
        energy.DepleateOverTime(.25f);

    }
}

[System.Serializable]
public class Need
{
    public float value;
    public float max;

    public Need(float max)
    {
        this.max = max;
        value = max;
    }

    public void DepleateOverTime(float strength)
    {
        if (value > 0)
        {
            value -= Time.deltaTime * strength;
        }
    }
}
