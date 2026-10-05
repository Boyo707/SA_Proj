using System.Collections.Generic;
using UnityEngine;

namespace PP.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private List<ItemData> itemDatas;

        [SerializeReference]
        private List<Item> items;

        public Item[] Items => items.ToArray();

        public static Inventory Instance;

        // MonoBehaviour-based sorting strategies.
        //[SerializeField]
        //private ItemSortingStrategy[] itemSortingStrategies;

        // Index of the currently active sorting strategy.
        //[SerializeField]
        //private int strategyIndex = 0;

        private void Awake()
        {
            GenerateInventory();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void GenerateInventory()
        {
            items = new List<Item>();
            foreach (ItemData itemData in itemDatas)
            {
                items.Add(itemData.CreateItem());
            }
        }

        // Adds an item to the inventory.
        public void AddItem(Item item)
        {
            items.Add(item);
        }
    }
}
