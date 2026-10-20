using PP.InventorySystem;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Item requiredItem;
    [SerializeField] private RoomController doorToRoom;

    public void OnInteraction()
    {
        Debug.Log("activating");
        doorToRoom.ActivateRoom();

        if (requiredItem == null)
        {
            doorToRoom.ActivateRoom();
        }
        //else if(required item found in inventory)
        //load in room 
    }
}
