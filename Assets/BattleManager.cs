using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public event Action onLoadingOverworld;
    public event Action onLoadingBattle;

    public int[] indexes = new int[3];

    public static BattleManager Instance;

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
    }

    public void LoadOverWorldScene()
    {
        StartCoroutine(loadOverWorld());
    }

    public void LoadBattleScene()
    {
        EventBus<onExitOverworldEventData>.Publish(new onExitOverworldEventData());
        SceneManager.LoadScene(1);
    }

    private IEnumerator loadOverWorld()
    {
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(0);
        yield return new WaitUntil(() => sceneAsync.isDone);
        EventBus<OnEnterOverworldEventData>.Publish(new OnEnterOverworldEventData());
    }
}
