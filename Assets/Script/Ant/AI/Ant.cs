using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class control the ant
/// </summary>
public class Ant : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 2;
    public float wanderStrength = 1;
    public float viewRadius = 2;
    public float viewAngle = 90f;
    public float smellRadius = 5;
    public float smellAngle = 180f;
    public float maxActivityTime = 60;
    public float timeBetweenPheramonePlacement = 2;
    public GameObject pheramonePrefab;

    public Vector2 position;

    Pool pheramonePool;
    public float timeSinceLastPheramonePlacement;


    public Task task;

    private void Awake()
    {
        pheramonePool = new Pool(10, pheramonePrefab);
        timeSinceLastPheramonePlacement = timeBetweenPheramonePlacement;
    }

    private void Update()
    {
        if (task == null)
        {
            task = new Task_Search_Food(this);
        }

        task.Update();
    }


    public void PlacePheramone(string msg, Color color)
    {
        if (timeSinceLastPheramonePlacement > timeBetweenPheramonePlacement)
        {
            GameObject pheramone = pheramonePool.Release();
            pheramone.transform.position = transform.position;
            pheramone.GetComponent<SpriteRenderer>().color = color;
            pheramone.GetComponent<Pheramone>().msg = msg;
            pheramone.GetComponent<Pheramone>().pool = pheramonePool;
            pheramone.GetComponent<Pheramone>().streangth = 15;
            timeSinceLastPheramonePlacement = 0;
        }
        else
        {
            timeSinceLastPheramonePlacement += Time.deltaTime;
        }
    }


    public Pheramone[] GetPheramonesOfType(string msg, bool isZone = false)
    {
        Collider2D[] allTargets =
            Physics2D.OverlapCircleAll
            (
                position,
                smellRadius,
                LayerMask.GetMask((isZone ? "Zone" : "phrm"))
            );

        if (allTargets.Length > 0)
        {
            //Filter pheramones by msg
            List<Pheramone> pheramones = new List<Pheramone>();

            foreach (Collider2D c in allTargets)
            {
                Pheramone pheramone = c.gameObject.GetComponent<Pheramone>();
                if (pheramone.msg == msg)
                {
                    pheramones.Add(pheramone);
                }
            }

            return pheramones.ToArray();
        }

        return null;
    }

    public bool IsSensingPheramone(string msg, bool isZone = false)
    {
        return GetPheramonesOfType(msg, isZone) != null && GetPheramonesOfType(msg).Length > 0;
    }

    public Pheramone GetPheramone(String msg, bool isZone = false)
    {
        Pheramone[] pheramones = GetPheramonesOfType(msg, isZone);
        if (pheramones.Length > 0 && pheramones != null)
        {
            return pheramones[UnityEngine.Random.Range(0, pheramones.Length)];
        }

        return null;
    }
}
