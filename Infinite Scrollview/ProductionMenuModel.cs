using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuModel : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    // List to store items shown in the production scroll view
    public List<GameObject> ItemPool { get; private set; } = new List<GameObject>();

    // Pools production items 5 times and adds them to the ItemPool
    public void PoolItems()
    {
        ItemPool.Clear();
        for (int a = 0; a < 5; a++)
        {
            for (int i = 0; i < items.Length; i++)
            {
                ItemPool.Add(items[i]);
            }
        }
    }
}

