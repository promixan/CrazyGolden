using System.Collections.Generic;
using UnityEngine;

public class ItemsPooler : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject poolTarget;
    [SerializeField] private List<GameObject> pooledItems;

    private void Start()
    {
        pooledItems = new List<GameObject>();
        for(var i = 0; i < poolSize; i++)
        {
            var item = Instantiate(poolTarget, transform);
            item.SetActive(false);
            pooledItems.Add(item);
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
    
    public void DeactivateAllItems()
    {
        foreach (var item in pooledItems)
        {
            item.gameObject.SetActive(false);
        }
    }
}
