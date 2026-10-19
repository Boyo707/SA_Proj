using PP.InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverWorldManager : MonoBehaviour
{
    public Dictionary<Vector3, RoomData> roomData = new Dictionary<Vector3, RoomData>();
    [SerializeField] public List<RoomData> listRoom = new List<RoomData>();

    private List<GameObject> tempRooms = new List<GameObject>(); 

    //[SerializeField]
    //private List<ItemData> itemDatas;

    //[SerializeReference]
    //private List<Item> items;

    //public Item[] Items => items.ToArray();

    public static OverWorldManager Instance;

    private void Awake()
    {
        //GenerateInventory();

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

    private void Start()
    {
        //GetRoomData();
    }

    public void GetRoomDataEvent(EventData eventData)
    {
        GetRoomData();
    }

    public void GetRoomData()
    {
        Debug.Log("Get room data");
        tempRooms.Clear();
        tempRooms = GameObject.FindGameObjectsWithTag("Room").ToList<GameObject>();
        for (int i = 0; i < tempRooms.Count; i++)
        {
            if (tempRooms[i].TryGetComponent<RoomController>(out RoomController room))
            {
                AddRoom(room);
            }
        }
    }

    public void SetRoomDataEvent(EventData eventData)
    {
        Debug.Log("set room data");
        tempRooms.Clear();
        tempRooms = GameObject.FindGameObjectsWithTag("Room").ToList<GameObject>();

        //EnemySpawner spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        for (int i = 0; i < tempRooms.Count; i++)
        {
            if (tempRooms[i].TryGetComponent<RoomController>(out RoomController room))
            {
                if (roomData.ContainsKey(room.transform.position))
                {
                    ///Give the room data
                    ///Give the enemy data
                    ///Give the chest data
                    
                    room.RoomData = roomData[room.transform.position];
                }
            }
        }
        //spawner.SpawnPreviousEnemies();
    }

    private void AddRoom(RoomController room)
    {
        if (!roomData.ContainsKey(room.transform.position))
        {
            roomData.Add(room.transform.position, room.GetRoomData());
            listRoom.Add(room.RoomData);
            
        }
        else
        {
            //room.RoomData = thisRoom;
            roomData[room.transform.position] = room.GetRoomData();
        }
    }

    public void OnEnable()
    {
        EventBus<onExitOverworldEventData>.OnEventPublished += GetRoomDataEvent;
        EventBus<OnEnterOverworldEventData>.OnEventPublished += SetRoomDataEvent;
    }
    public void OnDisable()
    {
        EventBus<onExitOverworldEventData>.OnEventPublished -= GetRoomDataEvent;
        EventBus<OnEnterOverworldEventData>.OnEventPublished -= SetRoomDataEvent;
    }
}
