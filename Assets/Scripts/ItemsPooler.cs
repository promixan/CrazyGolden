using System.Collections.Generic;
using UnityEngine;

public class ItemsPooler : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject poolTarget;
    [SerializeField] private List<GameObject> pooledItems;

    void Start()
    {
        pooledItems = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject gameObject = Instantiate(poolTarget);
            gameObject.SetActive(false);
            pooledItems.Add(gameObject);
            gameObject.transform.SetParent(this.transform);
        }
    }

    public GameObject GetAvailableItemFromPool()
    {
        foreach(GameObject item in pooledItems)
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }
}
