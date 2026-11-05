using System.Collections.Generic;
using UnityEngine;
using static TurnBasedController;

public class GameManager : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 companionPosition;

    [SerializeField] private CharacterData maleCharacter;
    [SerializeField] private CharacterData femaleCharacter;
    [SerializeField] private EnemyData battleEnemyblep;

    public Character maleData;
    public Character femaleData;

    public Enemy battleEnemyData;

    public static GameManager Instance; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        maleData = maleCharacter.CreateCharacter();
        femaleData = femaleCharacter.CreateCharacter();
        battleEnemyData = battleEnemyblep.CreateEnemy();

    }

    //eventually the void for starting the battle
    private void Start()
    {
        //activate camera
        //disable player controlls and enemy
        BattleManager.Instance.InitializeBattle(maleData, femaleData, battleEnemyData);
    }
}
