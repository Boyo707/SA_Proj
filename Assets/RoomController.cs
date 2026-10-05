using System.Collections.Generic;
using UnityEngine;


public class RoomController : MonoBehaviour
{
    [SerializeField] private RoomData roomData = new RoomData();
    public RoomData RoomData
    {
        get { return roomData; }
        set { roomData = value; }
    }

    [SerializeField] private List<GameObject> roomEnemies = new();
    public List<GameObject> RoomEnemies
    {
        get { return roomEnemies; }
    }

    private void Update()
    {
        if (RoomData.isDiscovered)
        {
            ActivateRoom();
        }
    }

    public void ActivateRoom()
    {
        if (roomEnemies.Count != 0)
        {
            for (int i = 0; i < roomEnemies.Count; i++)
            {
                roomEnemies[i].SetActive(true);
            }
        }
        if (roomData.chests.Count != 0)
        {
            for (int i = 0; i < roomData.chests.Count; i++)
            {
                roomData.chests[i].SetActive(true);
            }
        }
        roomData.isDiscovered = true;
    }

    public void AssignEnemy(GameObject pEnemies)
    {
        roomEnemies.Add(pEnemies);
    }

    public RoomData GetRoomData()
    {
        List<Enemy> enemies = new List<Enemy>();
        //Debug.Log(gameObject.name + ": " + roomEnemies.Count);
        for (int i = 0; i < roomEnemies.Count; i++)
        {
            //Debug.Log(roomEnemies[i].GetComponent<EnemyOverworldController>().Enemy.currentPosition);
            enemies.Add(roomEnemies[i].GetComponent<EnemyOverworldController>().Enemy);
        }
        roomData.enemies = enemies;
        return roomData;
    }
}
