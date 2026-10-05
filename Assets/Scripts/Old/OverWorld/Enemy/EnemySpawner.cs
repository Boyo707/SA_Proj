using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData[] enemyData;
    [SerializeField, Range(1, 6)] private int maxEnemyRoomAmount;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private RoomController[] rooms;

    private List<Enemy> enemyTypes = new List<Enemy>();
    private void Start()
    {
        if (OverWorldManager.Instance.roomData.Count == 0)
        {
            CreateEnemies();
            SpawnEnemies();
        }
        else
        {
            //SpawnPreviousEnemies();
        }
    }
    public void CreateEnemies()
    {
        Debug.Log("creating enemy types");
        for (int i = 0; i < enemyData.Length; i++)
        {
            Enemy newEnemy = enemyData[i].CreateEnemy();
            enemyTypes.Add(newEnemy);
        }
        enemyTypes.OrderByDescending(i => i.SpawnChance).ToList();
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            int spawnAmount = Random.Range(1, maxEnemyRoomAmount + 1);

            //Spawns the amount of enemies
            for (int j = 0;j < spawnAmount; j++)
            {
                Vector2 randomPos;

                Enemy spawnEnemyData = enemyTypes[0];
                int spawnChance = Random.Range(0, 100 + 1);
                randomPos = RandomPosition(rooms[i].transform);

                //Cycles through every enemy type and checks if the 'SpawnChance' is within the enemies SpawnChance
                for (int k = 0; k < enemyTypes.Count; k++)
                {
                    if (enemyTypes[k].SpawnChance > spawnChance)
                    {
                        spawnEnemyData = enemyTypes[k].Clone();
                    }
                    spawnEnemyData.currentPosition = randomPos;
                }

                //Spawns the enemyDefaultPrefab
                GameObject prefab = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                //Instantiates the script by giving the enemyData
                prefab.GetComponent<EnemyOverworldController>().InstantiateEnemy(spawnEnemyData);
                prefab.GetComponent<EnemyOverworldController>().roomKey = rooms[i].transform.position;
                //assigns an enemy to the parent room
                rooms[i].AssignEnemy(prefab);
            }
        }
    }
    public void SpawnPreviousEnemies()
    {
        Debug.Log("Spawning prev Enemies");
        for (int i = 0; i < rooms.Length; i++)
        {
            RoomData data = OverWorldManager.Instance.roomData[rooms[i].transform.position];
            for (int j = 0; j < data.enemies.Count; j++)
            {
                GameObject prefab = Instantiate(enemyPrefab, data.enemies[j].currentPosition, Quaternion.identity);
                prefab.GetComponent<EnemyOverworldController>().InstantiateEnemy(data.enemies[j]);
                rooms[i].AssignEnemy(prefab);
            }
        }
        
    }

    private Vector2 RandomPosition(Transform pTransform)
    {
        float x = Random.Range(pTransform.localPosition.x - pTransform.localScale.x/2, pTransform.localPosition.x + pTransform.localScale.x / 2);
        float y = Random.Range(pTransform.localPosition.y - pTransform.localScale.y/2, pTransform.localPosition.y + pTransform.localScale.y / 2);

        return new Vector2(x, y);
    }
}
