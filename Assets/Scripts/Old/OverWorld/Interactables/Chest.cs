using PP.InventorySystem;
using System;
using UnityEngine;



namespace PP.Interactables
{
    public class Chest : MonoBehaviour, IInteractable
    {
        public static Action<Item> onGetItem;

        [SerializeField] private ItemData itemData;
        private bool isOpened;
        public bool IsOpened => isOpened;
        public void OnInteraction()
        {
            if (!isOpened)
            {
                isOpened = true;
                Inventory.Instance.AddItem(itemData.CreateItem());
            }
        }
    }
}
