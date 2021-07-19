using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    Queue<GameObject> content;
    GameObject prefab;

    public Pool(int size, GameObject prefab)
    {
        this.content = new Queue<GameObject>();
        this.prefab = prefab;

        for (int i = 0; i < size; i++)
        {
            Add(GameObject.Instantiate(prefab));
        }
    }

    public void Add(GameObject gameObject)
    {
        content.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public GameObject Release()
    {
        GameObject gameObject;

        gameObject = content.Count > 0 ? content.Dequeue() : GameObject.Instantiate(prefab);
        gameObject.SetActive(true);
        return gameObject;
    }
}
