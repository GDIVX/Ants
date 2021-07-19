using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
     float timeBetweenPheramonePlacement = 8;
    public Color color;
    public string msg;
    public GameObject pheramonePrefab;

    private float timeSinceLastPheramonePlacement;
    Pool pheramonePool;

    private void Awake()
    {
        pheramonePool = new Pool(10, pheramonePrefab);
        timeSinceLastPheramonePlacement = timeBetweenPheramonePlacement;
    }
    // Update is called once per frame
    void Update()
    {
        PlacePheramone();
    }

    public void PlacePheramone()
    {
        if (timeSinceLastPheramonePlacement > timeBetweenPheramonePlacement)
        {
            GameObject pheramone = pheramonePool.Release();
            pheramone.transform.position = transform.position;

            pheramone.GetComponent<SpriteRenderer>().color = color;
            pheramone.GetComponent<Pheramone>().msg = msg;
            pheramone.GetComponent<Pheramone>().pool = pheramonePool;
            pheramone.GetComponent<Pheramone>().streangth = 4;
            timeSinceLastPheramonePlacement = 0;
        }
        else
        {
            timeSinceLastPheramonePlacement += Time.deltaTime;
        }
    }
}
