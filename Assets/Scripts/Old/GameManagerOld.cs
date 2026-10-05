using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerOld : MonoBehaviour
{
    [SerializeField]public Vector3 overWorldPosition = Vector3.zero;
    public Dictionary<Vector2, RoomData> roomData = new Dictionary<Vector2, RoomData>();
    [SerializeField] public List<RoomData> listRoom = new List<RoomData>();
    public int[] enemyIndexes = new int[3];


    [SerializeField] private CharacterDataOld[] data = new CharacterDataOld[2];
    [HideInInspector]public CharacterOld[] characterDatas = new CharacterOld[2];
    
    [HideInInspector] public Enemy currentEnemy;


    public event Action onLoadingOverworld;
    public event Action onLoadingBattle;

    //int is the objects instanceID, the value is the the room

    public static GameManagerOld Instance;

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
        }

        //gamemanager should contain the enemy attackin.
        for (int i = 0; i < data.Length; i++)
        {
            characterDatas[i] = data[i].CreateCharacter();
        }
    }

    public void OnEnterOverworld()
    {
        StartCoroutine(loadOverworld());
    }

    public void OnExitOveworld()
    {
        EventBus<onExitOverworldEventData>.Publish(new onExitOverworldEventData());
        SceneManager.LoadScene(1);
    }

    private IEnumerator loadOverworld()
    {
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(0);
        yield return new WaitUntil(() => sceneAsync.isDone);
        EventBus<OnEnterOverworldEventData>.Publish(new OnEnterOverworldEventData());
    }
}
