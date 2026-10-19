using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 companionPosition;

    [SerializeField] private CharacterData maleCharacter;
    [SerializeField] private CharacterData femaleCharacter;

    [HideInInspector] public Character maleData;
    [HideInInspector] public Character femaleData;

    public EnemyData battleEnemyblep;
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
}
