using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheramone : MonoBehaviour
{
    public string msg;
    public float streangth = 10f;
    public Pool pool;

    private void Update()
    {
        if (msg == "")
        {
            Debug.LogWarning("Empty Pheramone at " + transform.position.ToString());
            gameObject.SetActive(false);
            return;
        }

        if (streangth <= 0)
        {
            pool.Add(gameObject);
        }

        if (streangth != 999)
        {
            streangth -= Time.deltaTime;
        }
    }

}
