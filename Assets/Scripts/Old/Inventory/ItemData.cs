using System;
using UnityEngine;

namespace PP.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string ItemId;
        public string ItemName;
        public int Damage;
        public int Defense;
        public int healing;

        public Sprite ItemImage;

        public Item CreateItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        [SerializeField]
        private string id;
        public string Id => id;

        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
        [SerializeField]
        private int damage;
        public int Damage => damage;
        [SerializeField]
        private int defense;
        public int Defense => defense;

        [SerializeField]
        private int healing;
        public int Healing => healing;

        public Sprite ItemImage;

        public Item(ItemData itemData)
        {
            id = itemData.ItemId;
            itemName = itemData.ItemName;
            damage = itemData.Damage;
            defense = itemData.Defense;
            healing = itemData.healing;

            ItemImage = itemData.ItemImage;
        }
    }
}