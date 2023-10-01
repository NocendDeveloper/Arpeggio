using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviourDpm
{
    public GameObject prefab;
    public int poolSize;
    private GameObject[] itemPool;
    private int nextItemIndex;

    private void Start()
    {
        itemPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            itemPool[i] = GameObject.Instantiate(prefab, gameObject.transform);
            itemPool[i].SetActive(false);
        }
        nextItemIndex = 0;
    }
    public GameObject GetItem()
    {
        GameObject item;
        if (nextItemIndex == poolSize)
        {
            nextItemIndex = 0;
        }
        item = itemPool[nextItemIndex++];
        if (item.activeSelf)
        {
            item.SetActive(false);
        }
        item.SetActive(true);
        return item;
    }

    public GameObject GetItem(Vector3 position, Quaternion rotation)
    {
        GameObject item;
        if (nextItemIndex == poolSize)
        {
            nextItemIndex = 0;
        }
        item = itemPool[nextItemIndex++];
        if (item.activeSelf)
        {
            item.SetActive(false);
        }
        item.SetActive(true);
        item.transform.position = position;
        item.transform.rotation = rotation;
        return item;
    }
}
